using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventoura.Domain.Entities
{
    public class TourImage:BaseEntity
    {
        public string Url { get; set; }
        public bool? IsPrimary { get; set; }
        public Tour Tour { get; set; }
        public int TourId { get; set; }
    }
}
