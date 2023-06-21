using Core.Services.Form.Commands;
using Core.Services.Form.Queries;
using Core.Services.Form.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace DynamicForm.Controllers
{
    public class FormsController : BaseController<FormsController>
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveForm(FormRequest model)
        {
            try
            {
                dynamic res = new ExpandoObject();
                AddEditFormCommand command = new AddEditFormCommand(model);
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
        public async Task<IActionResult> GetAllForms()
        {
            try
            {
                dynamic res = new ExpandoObject();
                FormRequest formRequest = new FormRequest();

                var query = await _mediator.Send(new GetAllFormsQuery() { Where = "where status=1" });

                var mediatorResponse = await _mediator.Send(query);
                return Json(mediatorResponse);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
