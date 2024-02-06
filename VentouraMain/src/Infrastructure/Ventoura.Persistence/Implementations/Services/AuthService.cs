using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Users;
using Ventoura.Domain.Entities;

namespace Ventoura.Persistence.Implementations.Services
{
    public class AuthService:IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthService(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task Login(LoginVM loginVM)
        {
            AppUser user = await _userManager.FindByNameAsync(loginVM.UsernameOrEmail);
            if (user is null)
            {
                user = await _userManager.FindByEmailAsync(loginVM.UsernameOrEmail);
                if (user is null) throw new Exception("Email,Username or Password is incorrect");
            }
            if (!await _userManager.CheckPasswordAsync(user, loginVM.Password)) throw new Exception("Email,Username or Password is incorrect");
            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.IsRemembered, true);
            if (result.IsLockedOut) throw new Exception("Your Account Blocked because of Fail attempts,Please try later");
            if (!result.Succeeded)
            {
                 throw new Exception("Username,Email or Account is Incorrect");
            }
        }

        public async Task LogOut()
        {
           await _signInManager.SignOutAsync();
        }

        public async Task Register(RegisterVM registerVM)
        {
            if (await _userManager.Users.AnyAsync(u => u.UserName == registerVM.Username || u.Email == registerVM.Email)) throw new Exception("There is already a user in this email or username");
            AppUser user = new AppUser
            {
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                Email = registerVM.Email,
                UserName = registerVM.Username,
                Gender = registerVM.Gender
            };
            var result = await _userManager.CreateAsync(user, registerVM.Password);
            if (!result.Succeeded)
            {
                StringBuilder builder = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    builder.AppendLine(error.Description);
                }
                throw new Exception(builder.ToString());
            }
            await _signInManager.SignInAsync(user, false);
        }

        
    }
}
