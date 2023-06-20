using Microsoft.AspNetCore.Mvc;

namespace DynamicForm.Controllers
{
    public class FormsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
