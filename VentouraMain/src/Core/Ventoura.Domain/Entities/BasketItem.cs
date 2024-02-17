using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventoura.Domain.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
