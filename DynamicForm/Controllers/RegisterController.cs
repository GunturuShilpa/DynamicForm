using Microsoft.AspNetCore.Mvc;
using Core.Services.ApplicationUsers.Commands;
using Core.Services.ApplicationUsers.Requests;
using System.Dynamic;
using System.Security.Claims;

namespace DynamicForm.Controllers
{
    public class RegisterController : BaseController<FieldsController>
    {

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveUser(CreateUserRequest model)
        {
            //model.CreatedBy = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var command = new AddEditUsersCommand(model);
            var response = await _mediator.Send(command);
            dynamic result = new ExpandoObject();
            result.error = response.Succeeded;
            result.message = response.Messages.FirstOrDefault();

            if (response.Succeeded)
            {
                result.error = false;
            }
            else
            {
                result.error = true;
            }

            result.message = response.Messages.FirstOrDefault();

            return Json(result);
        }
    }
}
