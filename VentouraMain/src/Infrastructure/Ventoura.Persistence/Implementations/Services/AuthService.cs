using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.MyProfile;
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


        public async Task<AppUser> GetUserAsync(string userName)
        {
            return await _userManager.Users.Include(x => x).Include(x => x.BasketItems).FirstOrDefaultAsync(x => x.UserName == userName);
        }
        public async Task<ProfileUpdateVM> Updated(string username, ProfileUpdateVM vm)
        {
            if (username == null) throw new Exception("Bad Request");
            AppUser user = await GetUserAsync(username);
            if (user == null) throw new Exception("Not Found");
            vm.Name = user.Name;
            vm.Surname = user.Surname;
            vm.UserName = user.UserName;
            return vm;
        }

        public async Task<bool> LoginWith(string userName, ModelStateDictionary modelState)
        {
            AppUser user = await _userManager.FindByEmailAsync(userName);
            if (user is null)
            {
                user = await _userManager.FindByNameAsync(userName);
                if (user is null)
                {
                    modelState.AddModelError(string.Empty, "UserName , email or password is not true");
                }

            }
            await _signInManager.SignInAsync(user, true);
            return true;
        }

        public async Task<bool> Update(string username, ProfileUpdateVM vm, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;
            if (username == null) throw new Exception("Bad Request");
            AppUser existed = await GetUserAsync(username);
            if (existed == null) throw new Exception("Not Found");
            if (existed.UserName != vm.UserName)
            {
                if (await _userManager.Users.AnyAsync(x => x.UserName == vm.UserName))
                {
                    modelState.AddModelError("UserName", "This username is exis");
                    return false;
                }

            }
            existed.Name = vm.Name;
            existed.Surname = vm.Surname;
            existed.UserName = vm.UserName;
            var result = await _userManager.UpdateAsync(existed);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    modelState.AddModelError(string.Empty, item.Description);
                }
                return false;
            }
            return true;
        }
    }
}
