using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace DynamicForm.Controllers
{
    public class FieldsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
