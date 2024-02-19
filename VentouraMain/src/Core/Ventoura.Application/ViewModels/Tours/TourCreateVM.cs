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
        public int? Sale { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public int? DayCount { get; set; }
        public TimeSpan StartTime { get; set; }
        public List<Country>? Countries { get; set; }
        public int CountryId { get; set; }
        public int Capacity { get; set; }
        public List<City>? Cities { get; set; }
        public List<Category>? Categories { get; set; }
        public IFormFile MainPhoto { get; set; }
        public IFormFile HoverPhoto { get; set; }
        public List<IFormFile>? Photos { get; set; }
        // Relations
        //public Country Country { get; set; }
        public int CityId { get; set; }
        public int CategoryId { get; set; }
        //public City City { get; set; }
    }
}
