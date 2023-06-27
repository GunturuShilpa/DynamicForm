using AutoMapper;
using Core.Services.ControlFields.Queries;
using Core.Services.Form.Queries;
using Core.Services.TemplateFields.Commands;
using Core.Services.TemplateFields.Queries;
using Core.Services.TemplateFields.Requests;
using Core.Services.UserForm.Commands;
using Core.Services.UserForm.Requests;
using Core.Services.UserFormValue.Commands;
using Core.Services.UserFormValue.Requests;
using DynamicForm.Models;
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
       
        public async Task<IActionResult> Index(int id = 58)
        {           
            var mediatorResponse = await _mediator.Send(new GetAllFieldsQuery() { TemplateFormId = id });

            List<TemplateFieldsModel> formModel = (List<TemplateFieldsModel>)_mapper.Map<IEnumerable<TemplateFieldsModel>>(mediatorResponse.Data);


            GetAllControlTypesQuery getAllControlTypes = new GetAllControlTypesQuery();
            var fieldsData = await _mediator.Send(getAllControlTypes);
            var templateData = await _mediator.Send(new GetAllFormsQuery() { Where = "where status=1" });
            //TemplateFieldsModel formFields = new TemplateFieldsModel();
            foreach (TemplateFieldsModel formFields in formModel)
            {
                formFields.ControlName = fieldsData.Data.Where(x => x.Id == formFields.ControlId).FirstOrDefault().Name;
                formFields.TemplateName = templateData.Data.Where(x => x.Id == id).FirstOrDefault().Name;
                ViewBag.TemplateName = formFields.TemplateName;
            }
            //ViewBag.Options = "Telangana";

            return View(formModel);
        }        
    }
}
