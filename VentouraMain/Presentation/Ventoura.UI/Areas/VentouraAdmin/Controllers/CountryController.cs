﻿using Microsoft.AspNetCore.Mvc;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Countries;

namespace Ventoura.UI.Areas.VentouraAdmin.Controllers
{
    [Area("VentouraAdmin")]

    public class CountryController : Controller
    {
        private readonly ICountryService _service;
        public CountryController(ICountryService service)
        {
            _service = service;
        }
        public async Task<IActionResult> CountryTable(int page = 1, int take = 20)
        {
            return View(await _service.GetAllAsync(page, take));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CountryCreateVM countryCreateVM)
        {
            return View();
        }
    }
}
