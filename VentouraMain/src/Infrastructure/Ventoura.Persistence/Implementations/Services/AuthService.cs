using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Users;
using Ventoura.Domain.Entities;
using Ventoura.Domain.Enums;
using Ventoura.Domain.Extensions;
namespace Ventoura.Persistence.Implementations.Services
{
    public class AuthService:IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
      
        }
        public async Task CreateRoles()
        {
            foreach (var role in Enum.GetValues(typeof(UserRoles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString(),
                    });
                }
            }
        } 
        public async Task<bool> Login(LoginVM loginVM, ModelStateDictionary modelstate)
        {
            if (!modelstate.IsValid)
            {
                return false;
            }
            AppUser user = await _userManager.FindByNameAsync(loginVM.UsernameOrEmail);
            if (user is null)
            {
                user = await _userManager.FindByEmailAsync(loginVM.UsernameOrEmail);
                if (user is null)
                {
                    modelstate.AddModelError(String.Empty, "Email,Username or Password is incorrect");
                    return false;
                };
            }
            if (!await _userManager.CheckPasswordAsync(user, loginVM.Password))
            {
                modelstate.AddModelError(String.Empty, "Email,Username or Password is incorrect");
                return false;
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.IsRemembered, true);
            if (result.IsLockedOut)
            {
                modelstate.AddModelError(String.Empty, "Your Account Blocked because of Fail attempts,Please Try after 30 second");
                return false;
            }
            if (!result.Succeeded)
            {
                modelstate.AddModelError(String.Empty, "Username,Email or Account is Incorrect");
                return false;
            }
            return true;
        } 
        public async Task LogOut()
        {
           await _signInManager.SignOutAsync();
        }

        public async Task<bool> Register(RegisterVM registerVM, ModelStateDictionary modelstate)
        {
            if (!modelstate.IsValid)
            {
                return false;
            }
            if (!RegisterValidator.IsEmailValid(registerVM.Email))
            {
                modelstate.AddModelError("Email", "Invalid email format");
                return false;
            }
            if (RegisterValidator.IsDigit(registerVM.Name))
            {
                modelstate.AddModelError("Name", "You cannot use number in Name");
                return false;
            }
            if (RegisterValidator.IsDigit(registerVM.Surname))
            {   
                modelstate.AddModelError("Surname", "You cannot use number in Surname");
                return false;
            }
            if (RegisterValidator.ContainsSymbol(registerVM.Name))
            {
                modelstate.AddModelError("Name", "You cannot use symbol in Name");
                return false;
            }
            if (RegisterValidator.ContainsSymbol(registerVM.Surname))
            {
                modelstate.AddModelError("Surname", "You cannot use symbol in Surname");
                return false;
            }
            if (await _userManager.Users.AnyAsync(u => u.UserName == registerVM.Username || u.Email == registerVM.Email))
            {
                modelstate.AddModelError(String.Empty, "There is already a user in this email or username");
                return false;
            }
            AppUser user = new AppUser
            {
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                Email = registerVM.Email,
                UserName = registerVM.Username,
                Gender = registerVM.Gender
            };
            if (registerVM.Gender == GenderHelper.None)
            {
                modelstate.AddModelError("Gender", "Please select your gender.");
                return false;
            }
            var result = await _userManager.CreateAsync(user, registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    modelstate.AddModelError(String.Empty, error.Description);
                }
                return false;
            }
            await _userManager.AddToRoleAsync(user,UserRoles.Member.ToString());
            await _signInManager.SignInAsync(user, false);
            return true;
        }

        

    }
}