using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventoura.Domain.Entities
{
    public class Tour:BaseNameableEntity
    {
        public string? Description { get; set; }
        public string? Includes { get; set; }
        public string? IncludeDesc { get; set; }
        public decimal Price { get; set; }
        public decimal? SalePrice { get; set; }
        public int? Sale { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime StartDate { get; set; }
        public int? DayCount { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? AdultCount { get; set; }
        public int? ChildrenCount { get; set; }
        public List<TourImage>? TourImages { get; set; }

        // Relations
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }


    }
}
