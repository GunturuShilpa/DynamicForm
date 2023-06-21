using Core.Services.Form.Commands;
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
                return Json(mediatorResponse);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
