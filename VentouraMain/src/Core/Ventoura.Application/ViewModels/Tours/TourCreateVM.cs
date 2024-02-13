using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Domain.Entities;

namespace Ventoura.Application.ViewModels.Tours
{
    public class TourCreateVM
    {
        public string Name { get; set; }
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
        public List<Country>? Countries { get; set; }
        public List<City>? Cities { get; set; }
        public IFormFile MainPhoto { get; set; }
        public IFormFile HoverPhoto { get; set; }
        public List<IFormFile>? Photos { get; set; }
        // Relations
        [Required]
        public int? CountryId { get; set; }
        //public Country Country { get; set; }
        [Required]
        public int? CityId { get; set; }
        //public City City { get; set; }
    }
}
