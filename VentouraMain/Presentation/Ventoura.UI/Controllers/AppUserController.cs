using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Users;

namespace Ventoura.UI.Controllers
{
    //[Authorize(Roles = "Admin")]
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
            if (!await _service.Register(vm,ModelState))
            {
                return View(vm);
            }
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
            if (!await _service.Login(loginVM,ModelState))
            {
                return View(loginVM);
            } 
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> CreateRole()
        {
            await _service.CreateRoles();
            return View();
        }
    }
}
