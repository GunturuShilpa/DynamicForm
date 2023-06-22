using Core.Services.Form.Commands;
using Core.Services.Form.Queries;
using Core.Services.Form.Requests;
using Core.Services.TemplateFields.Commands;
using Core.Services.TemplateFields.Queries;
using Core.Services.TemplateFields.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;


namespace DynamicForm.Controllers
{
    public class FieldsController : BaseController<FieldsController>
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveField(FieldRequest model)
        {
            try
            {
                dynamic res = new ExpandoObject();
                AddEditFieldCommand command = new AddEditFieldCommand(model);
                var mediatorResponse = await _mediator.Send(command);

                if (mediatorResponse.Succeeded)
                {
                    res.error = false;
                }
                else
                {
                    res.error = true;
                }
                res.message = mediatorResponse.Messages.FirstOrDefault();

                return Json(res);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFields()
        {
            try
            {
                dynamic res = new ExpandoObject();
                FieldRequest formRequest = new FieldRequest();

                var mediatorResponse = await _mediator.Send(new GetAllFieldsQuery() { TemplateFormId = null });

                return Json(mediatorResponse);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
