﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.ViewModels.Cities;
using Ventoura.Application.ViewModels.Countries;
using Ventoura.Domain.Entities;

namespace Ventoura.Application.ViewModels.Tours
{
    public class TourGetVM
    {
        public int Id { get; set; }
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
        public string Url { get; set; }
        public bool? IsPrimary { get; set; }
        public Tour Tour { get; set; }
        public int TourId { get; set; }
        public List<TourImage> TourImages { get; set; }
        //Relationals
        public int CountryId { get; set; }
        public int CityId { get; set;}
        public int CategoryId { get; set; }
        public IncludeCountryVM Country { get; set; }
        public IncludeCityVM City { get; set; }
        public IncludeCategoryVM Category { get; set; }
        public List<Country>? Countries { get; set; }
		public int Capacity { get; set; }
		public int MemberCount { get; set; }
        public string Email { get; set; }

    }
}
