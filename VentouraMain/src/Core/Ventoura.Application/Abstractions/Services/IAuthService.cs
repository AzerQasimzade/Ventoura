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
        Task Register(RegisterVM registerVM);
        Task LogOut();
        Task Login(LoginVM loginVM);

    }
}
