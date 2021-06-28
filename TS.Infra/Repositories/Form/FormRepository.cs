using Dapper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Domain.Dtos.Form;
using TS.Domain.Entities.Form;
using TS.Infra.Base;
using TS.Infra.Context;

namespace TS.Infra.Repositories.Form
{
	public class FormRepository : IRepository
	{
		private readonly DbContext context;

		public FormRepository(DbContext context)
		{
			this.context = context;
		}

		public async Task<IEnumerable<FormComponentEntity>> GetFormComponents()
		{
			return await context.Connection.QueryAsync<FormComponentEntity>(@"SELECT * FROM FormComponent where IsActive = 1");
		}

		public async Task<IEnumerable<PropertyEntity>> GetComponentProperties(int formItemId)
		{
			return await context.Connection.QueryAsync<PropertyEntity>(@"SELECT FormItemPropertyId, PropertyName, PropertyType,  FormItemValue FROM FormItem fi
                join FormItemProperty fip on fip.FormItemIdFk = fi.FormItemId
                join FormProperty fp on fip.FormPropertyIdFk = fp.FormPropertyId
                where fi.FormItemId = @formItemId and fp.IsActive = 1", new { formItemId });
		}

		public async Task<int> IncludeNewForm(string formName)
		{
			int exists = await context.Connection.QueryFirstOrDefaultAsync<int>(@"select count(*) from Form where formName = @formName", new { formName });

			if (exists == 0)
			{
				await context.Connection.ExecuteAsync("Insert into Form (formName, formActive) values (@formName, 1)", new { formName });

				return await context.Connection.QueryFirstOrDefaultAsync<int>(@"Select FormId From Form where FormName = @formName", new { formName });
			}
			else
				return -1; //Form name already exists
		}

		public async Task<int> UpdateFormName(int formId, string formName)
		{
			int exists = await context.Connection.QueryFirstOrDefaultAsync<int>(@"select count(*) from Form where formName = @formName", new { formName });

			if (exists == 0)
			{
				await context.Connection.ExecuteAsync("Update Form set FormName = @formName where formId = @formId", new { formId, formName });

				return formId;
			}
			else
				return -1; //Form name already exists
		}

		public async Task<int> IncludeFormItem(int formId, int componentId, int? formItemParentId)
		{
			formItemParentId = formItemParentId == 0 ? null : formItemParentId;

			int formItemId = await context.Connection.QueryFirstAsync<int>(@"INSERT INTO FormItem (formidFk, FormComponentIdFk, FormItemIdFk) values (@formId, @componentId, @formItemParentId);
            SELECT CAST(SCOPE_IDENTITY() as int)", new { formId, componentId, formItemParentId });

			var properties = await context.Connection.QueryAsync<FormComponentProperty>(@"Select * From FormComponentProperty where FormComponentIdFk = @componentId", new { componentId });

			foreach (FormComponentProperty property in properties)
				await context.Connection.ExecuteAsync("INSERT INTO FormItemProperty (FormItemIdFk, FormPropertyIdFk, FormItemValue) values (@formItemId, @property, @defaultValue)", new { formItemId, property = property.FormPropertyIdFk, defaultValue = property.DefaultValue });


			return formItemId;
		}

		public async Task<int> UpdateFormItemParent(int formItemParentId, int formItemId)
		{
			await context.Connection.ExecuteAsync("UPDATE FormItem SET FormItemIdFk = @formItemParentId WHERE FormItemId = @formItemId", new { formItemParentId, formItemId });

			return formItemId;
		}

		public async Task<int> RemoveFormItem(int formItemId)
		{
			IEnumerable<int> children = await context.Connection.QueryAsync<int>("select formItemId from formItem where formItemIdFk = @formItemId", new { formItemId });

			//Deleting child item, this block makes a call to delete its own children
			foreach (int child in children)
				await RemoveFormItem(child);

			//Deleting properties
			await context.Connection.ExecuteAsync("Delete from FormItemProperty where formItemIdFk = @formItemId", new { formItemId });
			//Deleting parentItem
			await context.Connection.ExecuteAsync("Delete from FormItem where formItemId = @formItemId", new { formItemId });

			return formItemId;
		}

		//it should bring the form according to the menu associated
		public async Task<JsonFormDto> RetrieveJsonForm(int formId, int identifier = 0)
		{
			JsonFormDto jsonform = new JsonFormDto();

			var form = await context.Connection.QueryFirstOrDefaultAsync<FormEntity>(@"SELECT * FROM Form WHERE FormId = @formId", new { formId });

			if (form != null)
			{
				jsonform.Form_config = new Form_Config { Form_id = form.FormId, Form_name = form.FormName };
				jsonform.Form_fields = new Form_Fields { Items = await RetrieveFormItems(formId, null, identifier) };
			}


			return jsonform;
		}

		public async Task<List<JsonFormDto>> RetrieveJsonForm(int menuSubMenuId, bool menu, int identifier)
		{
			List<JsonFormDto> jsonform = new List<JsonFormDto>();
			IEnumerable<int> forms;

			if (menu)
			{
				forms = await context.Connection.QueryAsync<int>(@"SELECT FormId FROM Form f 
																				JOIN FormFormGroup ffg on f.FormId = ffg.FormIdFk
																				JOIN FormGroupMenu fgm on ffg.FormGroupId = fgm.FormGroupIdFk
																				WHERE fgm.MenusIdFk = @menuId", new { menuId = menuSubMenuId });
			}
			else
			{
				forms = await context.Connection.QueryAsync<int>(@"SELECT FormId FROM Form f 
																		JOIN FormFormGroup ffg on f.FormId = ffg.FormIdFk
																		JOIN FormGroupSubMenu fgsm on ffg.FormGroupId = fgsm.FormGroupIdFk
																		WHERE fgsm.SubMenusIdFk = @subMenusId", new { subMenusId = menuSubMenuId });
			}

			foreach (int form in forms)
				jsonform.Add(await RetrieveJsonForm(form, identifier));


			return jsonform;
		}

		public async Task<List<Form_Items>> RetrieveFormItems(int formId, int? formItemId = null, int identifier = 0)
		{
			List<Form_Items> form_items_object = new List<Form_Items>();

			var form_items = await context.Connection.QueryAsync<FormItemComponentEntity>($"SELECT FormItemId, FormItemIdFk, FormComponentIdFk, FormIdFk,ComponentContext, ComponentSubContext, ComponentType, AllowDrop From FormItem fi join FormComponent fc on fi.FormComponentIdFk = fc.FormComponentId where fi.FormIdFk = @formId and {(formItemId == null ? "FormItemIdFk is null" : "FormItemIdFk = @formItemId")}", new { formId, formItemId });

			foreach (FormItemComponentEntity form_item in form_items)
			{
				IEnumerable<FormItemProperty> properties = await GetFormPropertyValue(form_item.FormItemId);

				var children = await RetrieveFormItems(formId, form_item.FormItemId, identifier);

				string query = properties.Where(W => W.FormPropertyIdFk == 4).Select( s=> s.FormItemValue).FirstOrDefault();
				string itemName = properties.Where(W => W.FormPropertyIdFk == 1).Select(s => s.FormItemValue).FirstOrDefault();
				string itemTableField = properties.Where(W => W.FormPropertyIdFk == 11).Select(s => s.FormItemValue).FirstOrDefault();
				string dependsOn = properties.Where(W => W.FormPropertyIdFk == 12).Select(s => s.FormItemValue).FirstOrDefault();

				string defaultValue = "";

				if (!string.IsNullOrEmpty(query))
				{
					var items = await GetItemsWhenQuerysSpecified(query, form_item.ComponentContext, form_item.ComponentType, itemTableField, dependsOn, form_item.FormItemId, formId, identifier);
					if (items.Count() > 0)
						children.AddRange(items);
				}

				if (!string.IsNullOrEmpty(itemTableField) && identifier != 0 && (string.IsNullOrEmpty(query) || form_item.ComponentContext == 2))
				{
					var gettingFormPk = await context.Connection.QueryFirstOrDefaultAsync<string>($"SELECT formGroupQueryPk FROM FormGroup fg JOIN FormFormGroup ffg on fg.FormGroupId = ffg.FormGroupId WHERE ffg.formIdFk = {formId}");

					var splitedPk = gettingFormPk.Split('.');

					var splitedItemTableField = itemTableField.Split('.');

					string mountFieldQuery = $"SELECT {splitedItemTableField[1]} FROM {splitedItemTableField[0]} WHERE {splitedPk[1]} = {identifier}";

					defaultValue = await context.Connection.QueryFirstOrDefaultAsync<string>(mountFieldQuery);
				}

				form_items_object.Add(new Form_Items
				{
					ItemId = form_item.FormItemId,
					ItemContext = form_item.ComponentContext,
					ItemSubContext = form_item.ComponentsubContext,
					ItemType = form_item.ComponentType,
					ItemComponentId = form_item.FormComponentIdFk,
					ItemClassName = properties.Where(W => W.FormPropertyIdFk == 3).Select(s => s.FormItemValue).FirstOrDefault(),
					ItemPlaceholder = properties.Where(W => W.FormPropertyIdFk == 5).Select(s => s.FormItemValue).FirstOrDefault() ?? "",
					ItemDefaultValue = defaultValue,
					ItemLabel = properties.Where(W => W.FormPropertyIdFk == 2).Select(s => s.FormItemValue).FirstOrDefault(),
					ItemMaxLength = properties.Where(W => W.FormPropertyIdFk == 9).Select(s => s.FormItemValue).FirstOrDefault(),
					ItemName = itemName,
					ItemChecked = properties.Where(W => W.FormPropertyIdFk == 8).Select(s => s.FormItemValue).FirstOrDefault(),
					ItemReadOnly = properties.Where(W => W.FormPropertyIdFk == 7).Select(s => s.FormItemValue).FirstOrDefault(),
					ItemRowNumber = properties.Where(W => W.FormPropertyIdFk == 10).Select(s => s.FormItemValue).FirstOrDefault(),
					ItemRequired = properties.Where(W => W.FormPropertyIdFk == 6).Select(s => s.FormItemValue).FirstOrDefault(),
					ItemTableField = itemTableField,
					ItemChildren = children
				});
			}

			return form_items_object;
		}

		public async Task<List<Form_Items>> GetItemsWhenQuerysSpecified(string query, int itemContext, string itemType = "", string itemTableField = "", string dependsOn = "", int itemParentId = 0, int formId = 0, int identifier = 0)
		{
			List<Form_Items> form_items_object = new List<Form_Items>();

			var execute_reader = await context.Connection.ExecuteReaderAsync(query);

			DataTable form_items = new DataTable();
			form_items.Load(execute_reader);

			foreach (DataRow form_item in form_items.Rows)
			{
				string defaultValue = "";

				if (!string.IsNullOrEmpty(itemTableField) && identifier != 0 && itemContext != 2)
				{
					var splitedItemTableField = itemTableField.Split('.');

					var splitedDependsOn = dependsOn.Split('.');

					string mountFieldQuery = $"SELECT Count(*) FROM {splitedItemTableField[0]} WHERE {splitedDependsOn[2]} = {identifier} AND {splitedItemTableField[1]} = {(int)form_item[0]}";

					defaultValue = await context.Connection.QueryFirstOrDefaultAsync<int>(mountFieldQuery) > 0 ? "True" : "False";
				}


				form_items_object.Add(new Form_Items
				{
					ItemId = (int)form_item[0],
					ItemLabel = (string)form_item[1],
					ItemContext = itemContext,
					ItemType = itemType,
					ItemTableField = itemTableField,
					ItemParentId = itemParentId,
					ItemDefaultValue = defaultValue
				}); ;
				;
			}

			return form_items_object;
		}

		public async Task<string> GetFormPropertyValue(int formItemId, int formPropertyId)
		{
			var form_item_property = await context.Connection.QueryFirstOrDefaultAsync<string>($"SELECT FormItemValue FROM FormItemProperty Where FormItemIdFk = @formItemId and FormPropertyIdFk = @formPropertyId", new { formItemId, formPropertyId });

			return form_item_property;
		}
		public async Task<IEnumerable<FormItemProperty>> GetFormPropertyValue(int formItemId)
		{
			return await context.Connection.QueryAsync<FormItemProperty>($"SELECT * FROM FormItemProperty Where FormItemIdFk = @formItemId", new { formItemId });
		}

		public async Task<bool> UpdatePropertyValue(int formItemPropertyId, string value)
		{
			try
			{
				await context.Connection.ExecuteAsync(@"UPDATE FormItemProperty Set formItemValue = @value where FormItemPropertyId = @formItemPropertyId", new { formItemPropertyId, value });

				return true;
			}
			catch
			{
				return false;
			}

		}

		public async Task<IEnumerable<FormEntity>> FormList()
		{
			return await context.Connection.QueryAsync<FormEntity>("SELECT * FROM Form Order By FormId Desc");
		}

		public async Task<IEnumerable<string>> TableList()
		{
			return await context.Connection.QueryAsync<string>("SELECT TABLE_NAME FROM information_schema.tables WHERE TABLE_CATALOG = 'TS' ORDER BY TABLE_NAME");
		}

		public async Task<IEnumerable<string>> TableColumnList(string tableName)
		{
			return await context.Connection.QueryAsync<string>("SELECT TABLE_NAME+'.'+COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_CATALOG = 'TS' and TABLE_NAME = @tableName ORDER BY COLUMN_NAME", new { tableName });
		}

		public async Task<IEnumerable<FormGroupEntity>> FormGroupList()
		{
			return await context.Connection.QueryAsync<FormGroupEntity>("SELECT FormGroupId, FormGroupName FROM FormGroup");
		}

		public async Task RemoveAllFormTable(int formId)
		{
			await context.Connection.ExecuteAsync("DELETE FROM FormTable WHERE FormIdFk = @formId", new { formId });
		}

		public async Task IncludeFormTable(int formId, string tableName)
		{
			await context.Connection.ExecuteAsync("INSERT INTO FormTable (FormIdFk, FormTableName) Values (@formId, @tableName)", new { formId, tableName });
		}

		public async Task AssociateFormToGroup(int formId, int formGroup)
		{
			await context.Connection.ExecuteAsync("DELETE FROM FormFormGroup WHERE FormIdFk = @formId", new { formId });

			await context.Connection.ExecuteAsync("INSERT INTO FormFormGroup (FormIdFk, FormGroupId) values (@formId, @formGroup)", new { formId, formGroup });
		}

		public async Task<FormInfoDto> GetFormInfo(int formId)
		{
			FormInfoDto formInfoDto = new FormInfoDto();

			FormEntity form = await context.Connection.QueryFirstOrDefaultAsync<FormEntity>("SELECT * FROM Form WHERE FormId = @formId", new { formId });
			FormFormGroupEntity group = await context.Connection.QueryFirstOrDefaultAsync<FormFormGroupEntity>("SELECT * FROM FormFormGroup WHERE FormIdFk = @formId", new { formId });
			IEnumerable<FormTable> table = await context.Connection.QueryAsync<FormTable>("SELECT * FROM FormTable WHERE FormIdFk = @formId", new { formId });


			if (form != null)
			{
				formInfoDto.FormName = form.FormName;

				List<FormTableDto> tableDto = new List<FormTableDto>();

				foreach (var t in table)
				{
					tableDto.Add(new FormTableDto
					{
						Label = t.FormTableName,
						Value = t.FormTableName
					});
				}

				formInfoDto.FormTables = tableDto;
				formInfoDto.FormGroupId = group != null ? group.FormGroupId : 0;
			}

			return formInfoDto;
		}

		public async Task<FormResponseMessage> IncludeOrUpdateFormData(string json, int userId)
		{
			FormResponseMessage formResponse = new FormResponseMessage();
			IEnumerable<FormSubmitMessageEntity> formResponseMessages = null;
			int type = 1;

			try
			{
				List<FormIncludeOrUpdateData> formDataObj = new List<FormIncludeOrUpdateData>();

				var jdata = JObject.Parse(json);

				int formId = (int)jdata["formId"];
				int identifier = (int)jdata["identifier"];

				formResponse.Identifier = identifier;

				formResponseMessages = await context.Connection.QueryAsync<FormSubmitMessageEntity>("SELECT * FROM FormSubmitMessage WHERE FormIdFk = @formId", new { formId });

				var gettingFormPk = await context.Connection.QueryFirstOrDefaultAsync<string>($"SELECT formGroupQueryPk FROM FormGroup fg JOIN FormFormGroup ffg on fg.FormGroupId = ffg.FormGroupId WHERE ffg.formIdFk = {formId}");

				var splittedPk = gettingFormPk.Split('.');

				var formData = jdata["data"].Children();

				foreach (JToken data in formData)
				{
					List<FormItemChildren> itemChildren = new List<FormItemChildren>();
					JProperty jProperty = data.ToObject<JProperty>();
					string[] propertyName = jProperty.Name.Split('.');

					bool hasChildren = data.Children().Children().Count() > 0 ? true : false;

					string value = "";
					int formItemId = 0;

					if (hasChildren)
					{
						var children = data.Children().Children();

						foreach (var hadChild in children)
						{
							JProperty hadChildProperty = hadChild.ToObject<JProperty>();

							int id = Convert.ToInt32(hadChildProperty.Name);
							string childValue = hadChild.ToArray().FirstOrDefault().ToString();

							itemChildren.Add(new FormItemChildren
							{
								Id = id,
								ParentId = Convert.ToInt32(propertyName[2]),
								Value = childValue
							});
						}
					}
					else
					{
						formItemId = Convert.ToInt32(propertyName[2]);
						value = data.Children().FirstOrDefault().ToString();
					}


					formDataObj.Add(new FormIncludeOrUpdateData
					{
						Table = propertyName[0].ToLower(),
						Column = propertyName[1].ToLower(),
						ItemId = Convert.ToInt32(propertyName[2]),
						Value = value,
						HasChildren = hasChildren,
						Children = itemChildren
					});
				}

				//Separating tables

				var tables = from t in formDataObj
							 group t by new { t.Table }
							 into myTables
							 select myTables.FirstOrDefault();

				foreach (var table in tables)
				{
					var columnList = (await TableColumnList(table.Table)).ToList();

					var dependsOnUserId = columnList.Where(w => w.ToLower() == $"{table.Table.ToLower()}.userid").FirstOrDefault();

					string dependsOnUserIdColumn = "";
					string dependsOnUserIdValueInsert = "";
					string dependsOnUserIdValueUpdate = "";

					if (!string.IsNullOrEmpty(dependsOnUserId))
					{
						dependsOnUserIdColumn = $",{dependsOnUserId}";
						dependsOnUserIdValueInsert = $", {userId}";
						dependsOnUserIdValueUpdate = $"= {userId}";
					}

					var dependsOn = await GetFormPropertyValue(table.ItemId, 12);

					if (string.IsNullOrEmpty(dependsOn))
					{
						if (table.HasChildren)
						{
							foreach (var child in table.Children)
							{
								if (identifier == 0)
								{
									string query = $"INSERT INTO {table.Table} ({table.Column}{dependsOnUserIdColumn}) VALUES(";

									query += $"{child.Id}{dependsOnUserIdValueInsert})";

									await context.Connection.ExecuteAsync(query);

									string primaryKey = await context.Connection.QueryFirstOrDefaultAsync<string>($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE OBJECTPROPERTY(OBJECT_ID(CONSTRAINT_SCHEMA + '.' + QUOTENAME(CONSTRAINT_NAME)), 'IsPrimaryKey') = 1 AND TABLE_NAME = '{table.Table}'");

									formResponse.Identifier = await context.Connection.QueryFirstOrDefaultAsync<int>($"SELECT TOP 1 {primaryKey} FROM {table.Table} ORDER BY {primaryKey} DESC");
								}
								else
								{
									string query = $"UPDATE {table.Table} SET {table.Column} = {child.Id} {dependsOnUserIdColumn}{dependsOnUserIdValueUpdate} WHERE {gettingFormPk} = {identifier}";

									await context.Connection.ExecuteAsync(query);
								}
							}
						}
						else
						{
							var tableFields = (from t in formDataObj
											   where t.Table == table.Table
											   select t).ToList();

							if (identifier == 0)
							{
								string query = $"INSERT INTO {table.Table} (";

								var last = tableFields.Last();

								foreach (var tableField in tableFields)
								{
									if (!tableField.Equals(last))
										query += $"{tableField.Column},";
									else
										query += $"{tableField.Column}{dependsOnUserIdColumn}) VALUES (";
								}

								foreach (var tableField in tableFields)
								{
									string value = (int.TryParse(tableField.Value, out _) ? tableField.Value : (DateTime.TryParse(tableField.Value, out _) ? "'" + Convert.ToDateTime(tableField.Value.Replace("T", " ")).ToString("dd/MM/yyyy HH:ss") + "'" : "'" + tableField.Value + "'"));

									if (!tableField.Equals(last))
										query += $"{value},";
									else
										query += $"{value}{dependsOnUserIdValueInsert})";
								}

								await context.Connection.ExecuteAsync(query);

								string primaryKey = await context.Connection.QueryFirstOrDefaultAsync<string>($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE OBJECTPROPERTY(OBJECT_ID(CONSTRAINT_SCHEMA + '.' + QUOTENAME(CONSTRAINT_NAME)), 'IsPrimaryKey') = 1 AND TABLE_NAME = '{table.Table}'");

								formResponse.Identifier = await context.Connection.QueryFirstOrDefaultAsync<int>($"SELECT TOP 1 {primaryKey} FROM {table.Table} ORDER BY {primaryKey} DESC");
							}
							else
							{

								string query = $"UPDATE {table.Table} SET ";

								var last = tableFields.Last();

								foreach (var tableField in tableFields)
								{
									string value = (int.TryParse(tableField.Value, out _) ? tableField.Value : (DateTime.TryParse(tableField.Value, out _) ? "'" + Convert.ToDateTime(tableField.Value.Replace("T", " ")).ToString("dd/MM/yyyy HH:ss") + "'" : "'" + tableField.Value + "'"));

									if (!tableField.Equals(last))
										query += $"{tableField.Column} = {value},";
									else
										query += $"{tableField.Column} = {value}{dependsOnUserIdColumn}{dependsOnUserIdValueUpdate}";
								}

								query += $" WHERE {splittedPk[1]} = {identifier}";

								await context.Connection.ExecuteAsync(query);

								type = 2;
							}
						}
					}
					else
					{
						string[] tableColumnDependent = dependsOn.Split('.');

						int foreign_key = await context.Connection.QueryFirstOrDefaultAsync<int>($"SELECT TOP 1 {tableColumnDependent[1]} FROM {tableColumnDependent[0]} ORDER BY {tableColumnDependent[1]} DESC");

						formResponse.Identifier = foreign_key;

						if (table.HasChildren)
						{
							foreach (var child in table.Children)
							{
								if (identifier == 0)
								{
									string query = $"INSERT INTO {table.Table} ({table.Column}, {tableColumnDependent[2]}{dependsOnUserIdColumn}) VALUES(";

									query += $"{child.Id}, {foreign_key}{dependsOnUserIdValueInsert})";

									await context.Connection.ExecuteAsync(query);
								}
								else
								{
									string query = "";

									if (!Convert.ToBoolean(child.Value))
										query = $"DELETE FROM {table.Table} WHERE {tableColumnDependent[2]} = {identifier} AND {table.Column} = {child.Id}";
									else
									{
										string queryExists = $"SELECT COUNT(*) FROM {table.Table} WHERE {table.Column} = {child.Id} AND {tableColumnDependent[2]} = {identifier}";

										int exists = await context.Connection.QueryFirstOrDefaultAsync<int>(queryExists);

										if (exists == 0)
										{
											query = $"INSERT INTO {table.Table} ({table.Column}, {tableColumnDependent[2]}{dependsOnUserIdColumn}) VALUES(";

											query += $"{child.Id}, {identifier}{dependsOnUserIdValueInsert})";
										}
									}

									await context.Connection.ExecuteAsync(query);

									type = 2;
								}
							}
						}
						else
						{
							if (identifier == 0)
							{
								string query = $"INSERT INTO {table.Table} ({table.Column}, {tableColumnDependent[2]}{dependsOnUserIdColumn}) VALUES(";

								query += $"{table.Value}, {foreign_key}{dependsOnUserIdValueInsert})";

								await context.Connection.ExecuteAsync(query);
							}
							else
							{
								string query = "";

								if (!Convert.ToBoolean(table.Value))
									query = $"DELETE FROM {table.Table} WHERE {tableColumnDependent[2]} = {identifier} AND {table.Column} = {table.Value}";
								else
								{
									string queryExists = $"SELECT COUNT(*) FROM {table.Table} WHERE {table.Column} = {table.Value} AND {tableColumnDependent[2]} = {identifier}";

									int exists = await context.Connection.QueryFirstOrDefaultAsync<int>(queryExists);

									if (exists == 0)
									{
										query = $"INSERT INTO {table.Table} ({table.Column}, {tableColumnDependent[2]}{dependsOnUserIdColumn}) VALUES(";

										query += $"{table.Value}, {foreign_key}{dependsOnUserIdValueInsert})";
									}
								}

								await context.Connection.ExecuteAsync(query);

								type = 2;
							}
						}
					}
				}


			}
			catch (Exception e)
			{
				type = 3;
			}

			formResponse.MessageType = type;

			var response = (from frm in formResponseMessages
							where frm.MessageType == type
							select frm).FirstOrDefault();

			if (response != null)
			{
				formResponse.MessageType = response.MessageType;
				formResponse.Message = response.Message;
			}
			else if (type == 1)
				formResponse.Message = "Item successfully includeded!";
			else if (type == 2)
				formResponse.Message = "Item successfully updated!";
			else if (type == 3)
				formResponse.Message = "Ops, something went wrong! Contact the administrator!";

			return formResponse;
		}
	}
}
