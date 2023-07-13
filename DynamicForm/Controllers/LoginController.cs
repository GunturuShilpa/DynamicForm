using AutoMapper;
using Core.Services.Authentication.Commands;
using Core.Services.Authentication.Requests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DynamicForm.Controllers
{

    [AllowAnonymous]
    public class LoginController : BaseController<LoginController>
    {
        private IMapper _mapper;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IMapper mapper, ILogger<LoginController> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest loginModel)
        {
            if (ModelState.IsValid)
            {
                var data = new AuthenticateCommand(loginModel);
                var response = await _mediator.Send(data);
                if (response.Succeeded)
                {
                    //Create claims for user
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, Convert.ToString(response.Data.userId)),
                        new Claim(ClaimTypes.Name, response.Data.userName),
                        new Claim("Email", Convert.ToString(response.Data.email)),
                         new Claim("RoleId", Convert.ToString(response.Data.roleId))
                     };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        IsPersistent = true
                    });
                    HttpContext.Session.SetString("userId", response.Data.userName);
                    return RedirectToAction("Index", "Forms");
                }
                else
                {
                    ViewBag.Message = "Authentication failed. Please try again.";
                }

            }
            return View(loginModel);
        }

        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public PartialViewResult Register()
        {
            return PartialView("Register");
        }

    }
}
