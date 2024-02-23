using Microsoft.AspNetCore.Mvc;
using Stripe.Climate;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.MyProfile;

namespace Ventoura.UI.Controllers
{
    public class MyProfileController : Controller
    {
        public class ProfileController : Controller
        {
            private readonly IAuthService _authService;

            public ProfileController(IAuthService authService)
            {
                _authService = authService;
            }
            public async Task<IActionResult> Index()
            {
                return View(await _authService.GetUserAsync(User.Identity.Name));
            }
            //public async Task<IActionResult> Edit()
            //{
            //    return View(await _authService.Update(User.Identity.Name, new ProfileUpdateVM()));
            //}
            //[HttpPost]
            //public async Task<IActionResult> Edit(ProfileUpdateVM vm)
            //{
            //    if (!await _authService.Update(User.Identity.Name, vm, ModelState)) return View(await _authService.Updated(User.Identity.Name, vm));
            //    await _authService.LogOut();

            //    await _authService.LoginWith(vm.UserName, ModelState);
            //    return RedirectToAction(nameof(Index));
            //}
        }
    }
}
