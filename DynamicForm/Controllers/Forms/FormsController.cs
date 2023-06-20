using Microsoft.AspNetCore.Mvc;

namespace DynamicForm.Controllers.Forms
{
    public class FormsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
