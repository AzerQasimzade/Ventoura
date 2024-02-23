using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.ViewModels.MyProfile;
using Ventoura.Application.ViewModels.Users;
using Ventoura.Domain.Entities;

namespace Ventoura.Application.Abstractions.Services
{
    public interface IAuthService
    {
        Task<bool> Register(RegisterVM registerVM, ModelStateDictionary modelstate);
        Task LogOut();
        Task<bool> Login(LoginVM loginVM, ModelStateDictionary modelstate);
        Task CreateRoles();
        Task<AppUser> GetUserAsync(string userName);
        Task<bool> Update(string username, ProfileUpdateVM vm, ModelStateDictionary modelState);
        
            
    }
}

