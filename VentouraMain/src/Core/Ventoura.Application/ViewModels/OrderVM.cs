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
        public List<BasketItem> BasketItems { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public int Phone { get; set; }
        public string Country { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string OrderNote { get; set; }

    }
}
