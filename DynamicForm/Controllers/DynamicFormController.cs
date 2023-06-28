using AutoMapper;
using Core.Services.ControlFields.Queries;
using Core.Services.FieldOptions.Queries;
using Core.Services.Form.Queries;
using Core.Services.TemplateFields.Commands;
using Core.Services.TemplateFields.Queries;
using Core.Services.TemplateFields.Requests;
using Core.Services.UserForm.Commands;
using Core.Services.UserForm.Requests;
using Core.Services.UserFormValue.Commands;
using Core.Services.UserFormValue.Requests;
using DynamicForm.Models;
using Infrastructure.UserForm.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.Linq.Dynamic;

namespace DynamicForm.Controllers
{
    public class DynamicFormController :  BaseController<DynamicFormController>
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
            var templateData = await _mediator.Send(new GetAllFormsQuery() { Where = "where status=1" });
            if (formModel != null) { 
            foreach (TemplateFieldsModel formFields in formModel)
            {
                formFields.ControlName = fieldsData.Data.Where(x => x.Id == formFields.ControlId).FirstOrDefault().Name;
            }
            }
            ViewBag.TemplateFormId = id;
            ViewBag.TemplateName = templateData.Data.FirstOrDefault(x => x.Id == id).Name;
        

            return View(formModel);
        }
        [HttpPost]
        public async Task<IActionResult> SaveFormValues(IFormCollection formCollection)
        {
            dynamic result = new ExpandoObject();
                     
           
            int formId =Convert.ToInt32(formCollection["TemplateFormId"]);
            string name = string.Empty;
            string value = string.Empty;
            var response = (dynamic)null;

            var userFormData = new UserFormsRequest();
            userFormData.TemplateFormId = formId;
            userFormData.CreatedDate= DateTime.Now;

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
            return Json(result);
        }
    }
}
