using Microsoft.AspNetCore.Mvc;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Users;

namespace Ventoura.UI.Controllers
{
    public class AppUserController : Controller
    {
        private readonly IAuthService _service;

        public AppUserController(IAuthService service)
        {
            _service = service;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            await _service.Register(vm);
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> LogOut()
        {
            await _service.LogOut();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        { 
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            await _service.Login(loginVM);
            return RedirectToAction("Index", "Home");
        }
    }
}
