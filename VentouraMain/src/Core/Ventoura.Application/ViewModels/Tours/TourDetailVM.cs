﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.ViewModels.Cities;
using Ventoura.Application.ViewModels.Countries;
using Ventoura.Domain.Entities;

namespace Ventoura.Application.ViewModels.Tours
{
    public class TourDetailVM
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Includes { get; set; }
        public string Status { get; set; }
        public string? IncludeDesc { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public int? DayCount { get; set; }
        public TimeSpan StartTime { get; set; }
        public List<Country>? Countries { get; set; }
        public List<City>? Cities { get; set; }
        public List<Category> Categories { get; set; }
        public IFormFile? MainPhoto { get; set; }
        public IFormFile? HoverPhoto { get; set; }
        public List<IFormFile>? Photos { get; set; }
        public List<int>? ImageIds { get; set; }
        public List<TourImage>? TourImages { get; set; }
        public int CountryId { get; set; }
        // Relations
        public int CityId { get; set; }
        public int CategoryId { get; set; }
        public IncludeCityVM City { get; set; }
        public IncludeCountryVM Country { get; set; }
        public IncludeCategoryVM Category { get; set; }
		public int Capacity { get; set; } // Kontenjanı temsil eden özellik

	}
}
