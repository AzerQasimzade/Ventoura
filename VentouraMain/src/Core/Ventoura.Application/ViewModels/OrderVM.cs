using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Domain.Entities;

namespace Ventoura.Application.ViewModels
{
    public class OrderVM
    {
        public string Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public Tour Tour { get; set; }
        public int TourId { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
        public string Country { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public decimal Price { get; set; }
        public string OrderNote { get; set; }
        public int Count { get; set; }

        public string Email { get; set; }

    }
}
