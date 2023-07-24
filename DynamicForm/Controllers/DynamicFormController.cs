using AutoMapper;
using Core.Services.ControlFields.Queries;
using Core.Services.FieldOptions.Queries;
using Core.Services.FieldOptions.Responses;
using Core.Services.Form.Queries;
using Core.Services.TemplateFields.Queries;
using Core.Services.UserForm.Commands;
using Core.Services.UserForm.Requests;
using Core.Services.UserFormValue.Commands;
using Core.Services.UserFormValue.Queries;
using Core.Services.UserFormValue.Requests;
using DynamicForm.Filters;
using DynamicForm.Models;
using Infrastructure.BlobContainer.Repoistory;
using Infrastructure.FieldOptions.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Enum;
using System.Dynamic;
using System.Linq.Dynamic;
using System.Text.RegularExpressions;

namespace DynamicForm.Controllers
{
    public class DynamicFormController : BaseController<DynamicFormController>
    {
        private IMapper _mapper;
        private readonly IBlobContainerRepository _blobContainerRepository;

        public DynamicFormController(IMapper mapper, IBlobContainerRepository blobContainerRepository)
        {
            _mapper = mapper;
            _blobContainerRepository = blobContainerRepository;
        }
        [DecryptQueryStringParameter]
        public async Task<IActionResult> Index(string id)
        {
            int formId = int.Parse(id);
            var mediatorResponse = await _mediator.Send(new GetAllFieldsQuery() { TemplateFormId = formId });

            List<TemplateFieldsModel> formModel = (List<TemplateFieldsModel>)_mapper.Map<IEnumerable<TemplateFieldsModel>>(mediatorResponse.Data);

            var options = new List<SelectListItem>();
            var RadioButtonsOptions = new List<SelectListItem>();
            var CheckBoxListOptions = new List<SelectListItem>();

            GetAllControlTypesQuery getAllControlTypes = new GetAllControlTypesQuery();
            var fieldsData = await _mediator.Send(getAllControlTypes);

            var templateData = await _mediator.Send(new GetAllFormsQuery() { Where = "where Id =" + formId + " and status in (1,2)" });
            if (formModel.Count() > 0)
            {
                foreach (TemplateFieldsModel formFields in formModel)
                {
                    int FormFieldId = formModel.Where(x => x.ControlId == formFields.ControlId).First().Id;
                    var getOptions = await _mediator.Send(new GetAllFieldOptionsQuery() { Where = "where TemplateFormFieldId= " + FormFieldId + "and status=1" });
                    List<FieldOptionsResponse> Options = (List<FieldOptionsResponse>)_mapper.Map<IEnumerable<FieldOptionsResponse>>(getOptions.Data);
                    if (Options != null && Options.Count() > 0)
                    {
                        if (formFields.ControlId == (int)ControlType.Select)
                        {
                            foreach (var fieldoption in Options)
                            {
                                options.Add(new SelectListItem { Value = fieldoption.Id.ToString(), Text = fieldoption.OptionValue.ToString()});
                            }
                        }
                        if (formFields.ControlId == (int)ControlType.RadioButtonList)
                        {
                            foreach (var fieldoption in Options)
                            {
                                RadioButtonsOptions.Add(new SelectListItem { Value = fieldoption.Id.ToString(), Text = fieldoption.OptionValue.ToString(),Selected=fieldoption.Orientation});
                            }
                        }
                        if (formFields.ControlId == (int)ControlType.CheckBoxList)
                        {
                            foreach (var fieldoption in Options)
                            {
                                CheckBoxListOptions.Add(new SelectListItem { Value = fieldoption.Id.ToString(), Text = fieldoption.OptionValue.ToString(),Selected = fieldoption.Orientation});
                            }
                        }
                    }
                }
            }
            ViewBag.TemplateFormId = formId;
            if (templateData.Data.Count() > 0)
            {
                ViewBag.TemplateName = templateData.Data.First().Name;
            }

            ViewBag.TemplateFieldOptions = options;
            ViewBag.RadioButtonsOptions = RadioButtonsOptions;
            ViewBag.CheckBoxListOptions = CheckBoxListOptions;
            return View(formModel);
        }
        [HttpPost]
        public async Task<IActionResult> SaveFormValues(IFormCollection formCollection)
        {
            dynamic result = new ExpandoObject();
            int formId = Convert.ToInt32(formCollection["TemplateFormId"]);
            var userFormData = new UserFormsRequest();
            userFormData.TemplateFormId = formId;
            userFormData.CreatedDate = DateTime.UtcNow;
            bool IsValidationSuccess = true;
            var userFormValuesdata = new UserFormValuesRequest();
            var userFormcommand = new AddEditUserFormsCommand(userFormData);       
            var response = await _mediator.Send(userFormcommand);
            if (response.Succeeded)
            {
                if (formCollection.Count > 0)
                {
                    foreach (var key in formCollection.Keys.Skip(1))
                    {
                        var fieldsData = await _mediator.Send(new GetFieldDetailsById() { Where = "where TemplateFormId= " + formId + " and Name ='" + key + "' and Status = 1" });
                        if (fieldsData != null && fieldsData.Data.Count() > 0)
                        {
                            userFormValuesdata.TemplateFormId = formId;
                            userFormValuesdata.TemplateFormFieldId = fieldsData.Data.First().Id;
                            userFormValuesdata.FieldValue = formCollection[key].ToString();
                            var FieldName = fieldsData.Data.First().Name;
                            if (!String.IsNullOrEmpty(fieldsData.Data.First().RegExValue))
                            {
                                if (RegexCheck(userFormValuesdata.FieldValue, fieldsData.Data.First().RegExValue))
                                {
                                    var command = new AddEditUserFormValuesCommand(userFormValuesdata);
                                    await _mediator.Send(command);
                                    IsValidationSuccess = true;
                                    result.error = false;
                                }
                                else
                                {
                                    result.error = true;
                                    result.message = "Please enter valid " + FieldName + ".";
                                    IsValidationSuccess = false;
                                    break;
                                }
                            }
                            else
                            {
                                var command = new AddEditUserFormValuesCommand(userFormValuesdata);
                                await _mediator.Send(command);
                                result.error = false;
                            }
                        }
                    }
                }
                if (IsValidationSuccess)
                {
                    if (formCollection.Files.Count() > 0)
                    {
                        string imgURl = string.Empty;
                        foreach (var files in formCollection.Files)
                        {
                            var fieldsData = await _mediator.Send(new GetFieldDetailsById() { Where = "where TemplateFormId= " + formId + " and ControlId =" + (int)ControlType.FileUpload + " " });

                            imgURl = await _blobContainerRepository.CreateFileInPath(files);
                            if (fieldsData != null && fieldsData.Data.Count() > 0)
                            {
                                userFormValuesdata.TemplateFormId = formId;
                                userFormValuesdata.TemplateFormFieldId = fieldsData.Data.First().Id;
                                userFormValuesdata.FieldValue = imgURl;
                                var command = new AddEditUserFormValuesCommand(userFormValuesdata);
                                await _mediator.Send(command);
                            }
                        }
                        result.error = false;
                    }
                }
            }
            else
            {
                result.error = true;
            }           
            result.TemplateFormId = formId;
            return Json(result);
        }
        [HttpPost]
        public async Task<IActionResult> GetFormResponse(int id)
        {
            var formData = await _mediator.Send(new GetAllUserFormValues() { Where = "where TemplateFormId= " + id + "" });
            List<UserFormValuesModel> formValuesModel = (List<UserFormValuesModel>)_mapper.Map<IEnumerable<UserFormValuesModel>>(formData.Data);

            if (formValuesModel.Count() > 0)
            {
                foreach (UserFormValuesModel formFields in formValuesModel)
                {
                    if (formFields.TemplateFormFieldId != 0)
                    {
                        var fieldData = await _mediator.Send(new GetFieldDetailsById() { Where = "where  Id=" + formFields.TemplateFormFieldId + "" });
                        if (fieldData != null && fieldData.Data.Count() > 0)
                        {
                            formFields.FieldName = fieldData.Data.Where(x => x.Id == formFields.TemplateFormFieldId).First().Name;
                            formFields.FieldValue = formFields.FieldValue.ToString();
                        }
                    }
                }
            }

            return PartialView("ViewResponse", formValuesModel);
        }
        public bool RegexCheck(string Name, string RegexValue)
        {
            bool result = true;
            if (!string.IsNullOrEmpty(Name))
            {                
                Regex re = new Regex(@RegexValue);               
                if (!re.IsMatch(Name))
                {
                     result = false;
                }                
            }
            return result;
        }
    }
}
