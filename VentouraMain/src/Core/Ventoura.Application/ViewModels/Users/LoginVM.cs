using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventoura.Application.ViewModels.Users
{
    public class LoginVM
    {
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string UsernameOrEmail { get; set; }
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRemembered { get; set; }
        public bool LockOutOnFailure { get; set; }

    }
}
