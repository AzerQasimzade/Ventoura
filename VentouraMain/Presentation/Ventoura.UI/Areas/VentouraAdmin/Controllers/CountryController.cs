using Microsoft.AspNetCore.Mvc;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Cities;
using Ventoura.Application.ViewModels.Countries;
using Ventoura.Application.ViewModels.Tours;

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
        public async Task<IActionResult> Create()
        {
            return View(await _service.CreateGet(new CountryCreateVM()));
        }
        [HttpPost]
        public async Task<IActionResult> Create(CountryCreateVM countryCreateVM)
        {
            if(!await _service.Create(countryCreateVM, ModelState))
            {
                return View(countryCreateVM);
            }
            return RedirectToAction("CountryTable", "Country");
        }
    }
}
