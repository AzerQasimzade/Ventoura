using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Domain.Enums;

namespace Ventoura.Domain.Entities
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public GenderHelper Gender { get; set; }
        public bool IsActive { get; set; } = true;
        public List<WishlistItem> WishlistItems { get; set; }
        public List<BasketItem> BasketItems { get; set; }

    }
}
