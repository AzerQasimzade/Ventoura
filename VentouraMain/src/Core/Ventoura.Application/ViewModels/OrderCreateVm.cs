using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventoura.Application.ViewModels
{
    public class OrderCreateVm
    {
        [Required]
        [MinLength(3)]
        [MaxLength(27)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(29)]
        public string  LastName { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Address { get; set; }
        [Required]
        [MinLength(9)]
        [MaxLength(254)]
        public string UserEmail { get; set; }
        [Required]
        [RegularExpression("^(?:\\+994|0)(\\d{2})[- ]?(\\d{3})[- ]?(\\d{2})[- ]?(\\d{2})$")]
        public string UserPhoneNumber { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(500)]
        public string NotesForRestaurant { get; set; }
        public int CourierId { get; set; }
        public ICollection<string>? RestaurantAddreses { get; set; }
        //public ICollection<OrderItemVm> OrderItemVms { get; set; } = new List<OrderItemVm>();
    }
}
