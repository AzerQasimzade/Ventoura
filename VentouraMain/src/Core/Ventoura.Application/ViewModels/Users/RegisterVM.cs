using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Domain.Enums;

namespace Ventoura.Application.ViewModels.Users
{
    public class RegisterVM
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public GenderHelper Gender { get; set; }


    }
}
