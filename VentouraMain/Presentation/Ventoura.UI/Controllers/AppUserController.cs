using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Users;
using Ventoura.Domain.Entities;

namespace Ventoura.UI.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AppUserController : Controller
    {
        private readonly IAuthService _service;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMailService _mailService;

        public AppUserController(IAuthService service, UserManager<AppUser> userManager,IMailService mailService)
        {
            _service = service;
            _userManager = userManager;
            _mailService = mailService;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!await _service.Register(vm, ModelState))
            {
                return View(vm);
            }
            return RedirectToAction("Index", "Home");
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
            if (!await _service.Login(loginVM, ModelState))
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
        public async Task LoginWithGoogle()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("GoogleResponse")
                });
        }
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgot)
        {

            if (!ModelState.IsValid) return View(forgot);
            var user = await _userManager.FindByEmailAsync(forgot.Email);
            if (user is null) return NotFound();
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string link = Url.Action("ResetPassword", "AppUser", new { userId = user.Id, token = token }, HttpContext.Request.Scheme);
            await _mailService.SendEmailAsync(new MailRequestVM { ToEmail=forgot.Email,Subject="ResetPassword",Body=$"<a href='{link}'>ResetPassword</a>"});
            return RedirectToAction(nameof(Login));

        }
        public async Task<IActionResult> ResetPassword(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token)) return BadRequest();
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) return NotFound();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVM, string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token)) return BadRequest();
            if (!ModelState.IsValid) return View(resetPasswordVM);
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) return NotFound();
            var identityUser = await _userManager.ResetPasswordAsync(user, token, resetPasswordVM.ConfirmPassword);
            return RedirectToAction(nameof(Login));
        }
    }
}