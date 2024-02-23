using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Domain.Entities;

namespace Ventoura.Application.ViewModels.Tours
{
    public class TourReserveVM
    {
        public string? Name { get; set; }
        public int Id { get; set; }
        public int MemberCount { get; set; }
        public Tour? Tour { get; set; }
        public int TourId { get; set; }
        public int Capacity { get; set; }
        public string Email { get; set; }
        public DateTime StartDate { get; set; }
        //public string? Status { get; set; }
        public decimal Price { get; set; }

    }
}
