using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public AuthService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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
        }
    }
}
