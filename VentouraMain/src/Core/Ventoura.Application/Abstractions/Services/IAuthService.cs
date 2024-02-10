using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.ViewModels.Users;

namespace Ventoura.Application.Abstractions.Services
{
    public interface IAuthService
    {
        Task<bool> Register(RegisterVM registerVM, ModelStateDictionary modelstate);
        Task LogOut();
        Task<bool> Login(LoginVM loginVM, ModelStateDictionary modelstate);
        Task CreateRoles();
        Task<bool> ForgotPassword(ForgotPasswordVM forgot, ModelStateDictionary modelstate);
    }
}
