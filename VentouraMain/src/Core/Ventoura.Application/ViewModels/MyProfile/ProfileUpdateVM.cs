using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventoura.Application.ViewModels.MyProfile
{
    public class ProfileUpdateVM
    {
        [Required]
        [MinLength(3)]
        [MaxLength(27)]
        public string UserName { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(27)]
        public string Name { get; set; } = null!;
        [Required]
        [MinLength(3)]
        [MaxLength(27)]
        public string Surname { get; set; } = null!;
        [Required]
        public string ProfileImage { get; set; } = null!;
        public IFormFile? ProfilePhoto { get; set; }
    }
}
