using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TS.Domain.Dtos.Form;
using TS.Domain.Entities.Form;
using TS.Domain.Helper;
using TS.Infra.Repositories.Form;
using TS.Service.Base;
using TS_Security;

namespace TS.Service.Form
{
	public class FormService : IService
	{
		private readonly CacheHelper cacheHelper;
		private readonly FormRepository formRepository;
		private readonly AuthenticationTokenProvider authenticationToken;

		public FormService(CacheHelper cacheHelper, FormRepository formRepository, AuthenticationTokenProvider authenticationToken)
		{
			this.cacheHelper = cacheHelper;
			this.formRepository = formRepository;
			this.authenticationToken = authenticationToken;
		}

		public async Task<IEnumerable<FormComponentDto>> GetFormComponents()
		{
			var list = await formRepository.GetFormComponents();

			return list.Select(FormComponentDto.From);
		}

		public async Task<IEnumerable<ComponentPropertiesDto>> GetComponentProperties(int formItemId)
		{
			var list = await formRepository.GetComponentProperties(formItemId);

			return list.Select(ComponentPropertiesDto.From);
		}

		public async Task<int> IncludeNewForm(string formName)
		{
			return await formRepository.IncludeNewForm(formName);
		}

		public async Task<int> UpdateFormName(int formId, string formName)
		{
			return await formRepository.UpdateFormName(formId, formName);
		}

		public async Task<int> IncludeFormItem(int formId, int componentId, int? formItemParentId)
		{
			return await formRepository.IncludeFormItem(formId, componentId, formItemParentId);
		}

		public async Task<int> UpdateFormItemParent(int formItemParentId, int formItemId)
		{
			return await formRepository.UpdateFormItemParent(formItemParentId, formItemId);
		}

		public async Task<int> RemoveFormItem(int formItemId)
		{
			return await formRepository.RemoveFormItem(formItemId);
		}

		public async Task<JsonFormDto> RetrieveJsonForm(int formId)
		{
			var cacheKey = $"cache-jsonform-{formId}";

			var cacheItem = cacheHelper.Get(cacheKey);

			if (string.IsNullOrEmpty(cacheItem) == true)
			{
				var jsonForm = await formRepository.RetrieveJsonForm(formId);

				var cacheItemSerialized = JsonConvert.SerializeObject(jsonForm);

				cacheHelper.Set(cacheKey, cacheItemSerialized);

				return jsonForm;
			}
			else
			{
				return JsonConvert.DeserializeObject<JsonFormDto>(cacheItem);
			}
		}

		public async Task<List<JsonFormDto>> RetrieveJsonForm(int menuSubMenuId, bool menu, int identifier)
		{
			var cacheKey = $"cache-jsonform-{menuSubMenuId}";

			var cacheItem = cacheHelper.Get(cacheKey);

			if (string.IsNullOrEmpty(cacheItem) == true)
			{
				var jsonForm =  await formRepository.RetrieveJsonForm(menuSubMenuId, menu, identifier);

				var cacheItemSerialized = JsonConvert.SerializeObject(jsonForm);

				cacheHelper.Set(cacheKey, cacheItemSerialized);

				return jsonForm;
			}
			else
			{
				return JsonConvert.DeserializeObject<List<JsonFormDto>>(cacheItem);
			}
		}

		public async Task<bool> UpdatePropertyValue(int formItemPropertyId, string value)
		{
			return await formRepository.UpdatePropertyValue(formItemPropertyId, value);
		}

        public async Task<IEnumerable<FormEntity>> FormList()
        {
			return await formRepository.FormList();
        }

        public async Task<IEnumerable<string>> TableList()
        {
			return await formRepository.TableList();
        }

        public async Task<IEnumerable<string>> TableColumnList(string tableName)
        {
			return await formRepository.TableColumnList(tableName);
        }

        public async Task<IEnumerable<FormGroupEntity>> FormGroupList()
        {
			return await formRepository.FormGroupList();
        }

        public async Task IncludeFormTable(string json)
        {
			var data = JObject.Parse(json);

			int formId = (int)data["formId"];

			var tables = data["associatedTables"].ToArray();

           
			await formRepository.RemoveAllFormTable(formId);

			foreach (var table in tables) {
				await formRepository.IncludeFormTable(formId, table["value"].ToString());
            }
		}

		public async Task AssociateFormToGroup(int formId, int formGroup)
        {
			await formRepository.AssociateFormToGroup(formId, formGroup);
        }

        public async Task<FormInfoDto> GetFormInfo(int formId)
        {
			return await formRepository.GetFormInfo(formId);
        }

		public async Task<FormResponseMessage> IncludeOrUpdateFormData(string json,string token)
		{
			int userId = Convert.ToInt32(authenticationToken.DecriptToken(token));

			return await formRepository.IncludeOrUpdateFormData(json, userId);
		}
	}
}
