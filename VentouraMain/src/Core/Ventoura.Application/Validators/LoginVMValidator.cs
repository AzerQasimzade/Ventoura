using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.ViewModels.Users;

namespace Ventoura.Application.Validators
{
    public class LoginVMValidator:AbstractValidator<LoginVM>
    {
        public LoginVMValidator()
        {
            RuleFor(x => x.UsernameOrEmail)
                .NotEmpty()
                .MaximumLength(256)
                .MinimumLength(4);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(100);
        }
    }
}
