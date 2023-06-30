using AutoMapper;
using Core.Services.ControlFields.Queries;
using Core.Services.Form.Queries;
using Core.Services.TemplateFields.Queries;
using Core.Services.UserForm.Commands;
using Core.Services.UserForm.Requests;
using Core.Services.UserFormValue.Commands;
using Core.Services.UserFormValue.Queries;
using Core.Services.UserFormValue.Requests;
using DynamicForm.Models;
using Infrastructure.UserForm.Entity;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using System.Dynamic;
using System.Linq.Dynamic;

namespace DynamicForm.Controllers
{
    public class DynamicFormController : BaseController<DynamicFormController>
    {
        private IMapper _mapper;

        public DynamicFormController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int id = 0)
        {
            var mediatorResponse = await _mediator.Send(new GetAllFieldsQuery() { TemplateFormId = id });

            List<TemplateFieldsModel> formModel = (List<TemplateFieldsModel>)_mapper.Map<IEnumerable<TemplateFieldsModel>>(mediatorResponse.Data);


            GetAllControlTypesQuery getAllControlTypes = new GetAllControlTypesQuery();
            var fieldsData = await _mediator.Send(getAllControlTypes);

            var templateData = await _mediator.Send(new GetAllFormsQuery() { Where = "where Id =" + id + " and status=1" });
            if (formModel.Count() > 0)
            {
                foreach (TemplateFieldsModel formFields in formModel)
                {
                    formFields.ControlName = fieldsData.Data.Where(x => x.Id == formFields.ControlId).FirstOrDefault().Name;
                }
            }
            ViewBag.TemplateFormId = id;
            if (templateData.Data.Count() > 0)
            {
                ViewBag.TemplateName = templateData.Data.FirstOrDefault(x => x.Id == id).Name;
            }


            return View(formModel);
        }
        [HttpPost]
        public async Task<IActionResult> SaveFormValues(IFormCollection formCollection)
        {
            dynamic result = new ExpandoObject();


            int formId = Convert.ToInt32(formCollection["TemplateFormId"]);
            string name = string.Empty;
            string value = string.Empty;
            var response = (dynamic)null;

            var userFormData = new UserFormsRequest();
            userFormData.TemplateFormId = formId;
            userFormData.CreatedDate = DateTime.Now;

            var userFormValuesdata = new UserFormValuesRequest();
            var userFormcommand = new AddEditUserFormsCommand(userFormData);
            response = await _mediator.Send(userFormcommand);
            if (response.Succeeded)
            {
                if (formCollection.Count > 0)
                {
                    foreach (var key in formCollection.Keys.Skip(1))
                    {
                        //name = "Key=" + key + " ";
                        //value = "Value=" + formCollection[key];                       

                        var fieldsData = await _mediator.Send(new GetFieldDetailsById() { Where = "where TemplateFormId= " + formId + " and Name ='" + key + "'" });

                        userFormValuesdata.TemplateFormId = formId;
                        userFormValuesdata.TemplateFormFieldId = (fieldsData.Data.Count() > 0) ? fieldsData.Data.FirstOrDefault().Id : 0;
                        userFormValuesdata.FieldValue = formCollection[key].ToString();

                        var command = new AddEditUserFormValuesCommand(userFormValuesdata);
                         response = await _mediator.Send(command);
                    }
                }
                result.error = false;
            }
            else
            {
                result.error = true;
            }
            result.error = false;
            result.TemplateFormId = formId;
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetFormResponse(int id)
        {            
            var formData= await _mediator.Send(new GetAllUserFormValues() { Where = "where TemplateFormId= " + id + "" });
            List<UserFormValuesModel> formValuesModel = (List<UserFormValuesModel>)_mapper.Map<IEnumerable<UserFormValuesModel>>(formData.Data);

            if (formValuesModel.Count() > 0)
            {
                foreach (UserFormValuesModel formFields in formValuesModel)
                {
                    if (formFields.TemplateFormFieldId != 0)
                    {
                        var fieldData = await _mediator.Send(new GetFieldDetailsById() { Where = "where  Id=" + formFields.TemplateFormFieldId + "" });
                        formFields.FieldName = fieldData.Data.Where(x => x.Id == formFields.TemplateFormFieldId).FirstOrDefault().Name;
                        formFields.FieldValue = formFields.FieldValue.ToString();
                    }
                }
            }

            return PartialView("ViewResponse", formValuesModel);
        }
    }
}
