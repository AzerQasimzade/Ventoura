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
        public async Task<IActionResult> Register([FromForm] RegisterVM vm)
        {
            await _service.Register(vm);
            return View(vm);
        }
    }
}
