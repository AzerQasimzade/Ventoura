using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventoura.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public List<WishlistItem> WishlistItems { get; set; }
        public decimal TotalPrice { get; set; }
        public bool? Status { get; set; }
        public string FirstName { get; set; }   
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public int Phone { get; set; }
        public string Country { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string OrderNote { get; set; }
        public DateTime PurchasedAt { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }



    }
}
