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
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction("CountryTable", "Country");
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            return View(await _service.UpdateGet(id, new CountryUpdateVM()));
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, CountryUpdateVM vm)
        {
            if (id <= 0) return BadRequest();
            if (!await _service.Update(id, vm, ModelState)) return View(vm);
            return RedirectToAction("CountryTable", "Country");
        }
        public async Task<IActionResult> Details(int id)
        {

            if (id <= 0) return BadRequest();
            var tour = await _service.GetByIdAsync(id);
            if (tour == null)
            {
                ModelState.AddModelError(string.Empty, "The product you are looking for is no longer available");
                return View("Error");
            }
            return View(await _service.GetDetail(id, new CountryDetailVM()));


        }
    }
}
