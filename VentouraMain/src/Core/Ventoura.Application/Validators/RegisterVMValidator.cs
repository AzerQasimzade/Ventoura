using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.ViewModels.Users;

namespace Ventoura.Application.Validators
{
    public class RegisterVMValidator:AbstractValidator<RegisterVM>
    {
        public RegisterVMValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(256)
                .Matches(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(100);
            RuleFor(x => x.Username)
                .NotEmpty()
                .MaximumLength(256)
                .MinimumLength(8);
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50)
                .MinimumLength(3)
                .Matches(@"^[a-zA-Z\s]*$");
            RuleFor(x => x.Surname)
               .NotEmpty()
               .MaximumLength(50)
               .MinimumLength(3)
               .Matches(@"^[a-zA-Z\s]*$");
            RuleFor(x => x)
                .Must(x => x.ConfirmPassword == x.Password);
            RuleFor(x => x)
                .NotEmpty();
        }
    }
}
