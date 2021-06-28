using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Annotations;
using TS.Service.Form;

namespace TS_Api.Controllers
{
    [Route("Form")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly FormService formService;

        public FormController(FormService formService)
        {
            this.formService = formService;
        }

        [HttpGet("FormComponents")]
        [SwaggerOperation(Summary = "Form html components", Description = "Returns all components available to create forms")]
        [SwaggerResponse(200, "List successfully retrieved")]
        public async Task<IActionResult> GetFormComponents()
        {
            try
            {
                return Ok(await formService.GetFormComponents());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("ComponentsProperties")]
        [SwaggerOperation(Summary ="Html form components properties", Description ="Brings all the properties defined to the componentes required")]
        [SwaggerResponse(200, "Component succesfully retrieved")]
        public async Task<IActionResult> GetComponentProperties(int formItemId)
        {
            try
            {
                return Ok(await formService.GetComponentProperties(formItemId));
            }catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("IncludeNewForm")]
        [SwaggerOperation(Summary = "New Form Inclusion", Description = "Includes a new formName into the system")]
        [SwaggerResponse(200, "Component succesfully retrieved")]
        public async Task<IActionResult> IncludeNewForm(string formName)
        {
            try
            {
                return Ok(await formService.IncludeNewForm(formName));
            }catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("UpdateFormName")]
        [SwaggerOperation(Summary = "Updates form name", Description = "Updates the form name if the name ins't already taken, if so will return -1 value or else the current form id")]
        [SwaggerResponse(200, "Component succesfully retrieved")]
        public async Task<IActionResult> UpdateFormName(int formId, string formName)
        {
            try
            {
                return Ok(await formService.UpdateFormName(formId, formName));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("IncludeFormItem")]
        [SwaggerOperation(Summary = "Includes a form item", Description = "Includes a new form item")]
        [SwaggerResponse(200, "Component succesfully retrieved")]
        public async Task<IActionResult> IncludeFormItem(int formId, int componentId, int? formItemParentId)
        {
            try
            {
                return Ok(await formService.IncludeFormItem(formId,componentId,formItemParentId));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("RemoveFormItem")]
        [SwaggerOperation(Summary = "Removes a form item", Description = "Removes a new form item")]
        [SwaggerResponse(200, "Component succesfully retrieved")]
        public async Task<IActionResult> RemoveFormItem(int formItemId)
        {
            try
            {
                return Ok(await formService.RemoveFormItem(formItemId));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("RetrieveJsonForm")]
        [SwaggerOperation(Summary = "Retrieves form items", Description = "Gets all the form structure, based on the data included")]
        [SwaggerResponse(200, "Component succesfully retrieved")]
        public async Task<IActionResult> RetrieveJsonForm(int formId)
        {
            try
            {
                return Ok(await formService.RetrieveJsonForm(formId));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("RetrieveJsonFormByMenu")]
        [SwaggerOperation(Summary = "Retrieves list of form items", Description = "Gets all the forms associated to the menu")]
        [SwaggerResponse(200, "Component succesfully retrieved")]
        public async Task<IActionResult> RetrieveJsonFormByMenu(int menuSubMenuId, bool menu, int identifier)
        {
            try
            {
                return Ok(await formService.RetrieveJsonForm(menuSubMenuId, menu, identifier));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("UpdatePropertyValue")]
        [SwaggerOperation(Summary = "updates properties value", Description = "Updates the property value according to the Id passed")]
        [SwaggerResponse(200, "Component succesfully retrieved")]
        public async Task<IActionResult> UpdatePropertyValue(int formItemPropertyId, string value)
        {
            try
            {
                return Ok(await formService.UpdatePropertyValue(formItemPropertyId, value));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("UpdateFormItemParent")]
        [SwaggerOperation(Summary = "updates item parent id", Description = "Updates the of the item when the item is dragged into another item")]
        [SwaggerResponse(200, "Component succesfully retrieved")]
        public async Task<IActionResult> UpdateFormItemParent(int formItemParentId, int formItemId)
        {
            try
            {
                return Ok(await formService.UpdateFormItemParent(formItemParentId, formItemId));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("FormList")]
        [SwaggerOperation(Summary = "gets form list", Description = "Gets all the forms included into the database")]
        [SwaggerResponse(200, "Component succesfully retrieved")]
        public async Task<IActionResult> FormList()
        {
            try
            {
                return Ok(await formService.FormList());
            }catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("TableList")]
        [SwaggerOperation(Summary = "gets the database tables", Description = "Gets all the table names from database")]
        [SwaggerResponse(200, "Component succesfully retrieved")]
        public async Task<IActionResult> TableList()
        {
            try
            {
                return Ok(await formService.TableList());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("TableColumnList")]
        [SwaggerOperation(Summary = "gets the database table columns", Description = "Gets all the table columns names from database")]
        [SwaggerResponse(200, "Component succesfully retrieved")]
        public async Task<IActionResult> TableColumnList(string tableName)
        {
            try
            {
                return Ok(await formService.TableColumnList(tableName));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("FormGroupList")]
        [SwaggerOperation(Summary = "gets the form groups", Description = "Gets all the form groups available to be used")]
        [SwaggerResponse(200, "Component succesfully retrieved")]
        public async Task<IActionResult> FormGroupList()
        {
            try
            {
                return Ok(await formService.FormGroupList());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("IncludeFormTable")]
        [SwaggerOperation(Summary = "Associates the form with the table", Description = "Includes all the tables related to the passed form")]
        [SwaggerResponse(200, "Component succesfully retrieved")]
        public async Task<IActionResult> IncludeFormTable([FromBody] JsonElement content)
        {
            try
            {
                string json = JsonSerializer.Serialize(content);

                await formService.IncludeFormTable(json);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("AssociateFormToGroup")]
        [SwaggerOperation(Summary = "Associates form to a group", Description = "Associates form to a group")]
        [SwaggerResponse(200, "Component succesfully retrieved")]
        public async Task<IActionResult> AssociateFormToGroup(int formId, int formGroupId)
        {
            try
            { 
                await formService.AssociateFormToGroup(formId, formGroupId);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("GetFormInfo")]
        [SwaggerOperation(Summary = "Gets form info", Description = "Gets formName, form tables associated, group associated and query")]
        [SwaggerResponse(200, "Info succesfully retrieved")]
        public async Task<IActionResult> GetFormInfo(int formId)
        {
            try
            {
                return Ok(await formService.GetFormInfo(formId));
            }catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("IncludeOrUpdateFormData")]
        [SwaggerOperation(Summary = "Includes or updates form info", Description = "This method will include form info in case of some statements be satisfied, or else it will update the info")]
        [SwaggerResponse(200, "Data succesfully inserted or updated")]
        public async Task<IActionResult> IncludeOrUpdateFormData([FromBody] JsonElement content)
        {
            try
            {
                string authHeader = HttpContext.Request.Headers["Authorization"].ToString().Split(" ").LastOrDefault();

                string json = JsonSerializer.Serialize(content);

                return Ok(await formService.IncludeOrUpdateFormData(json, authHeader));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}