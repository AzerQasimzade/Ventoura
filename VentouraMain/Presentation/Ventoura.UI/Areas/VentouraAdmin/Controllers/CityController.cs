using Microsoft.AspNetCore.Mvc;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Cities;
using Ventoura.Application.ViewModels.Tours;

namespace Ventoura.UI.Areas.VentouraAdmin.Controllers
{
    [Area("VentouraAdmin")]
    public class CityController : Controller
    {
        private readonly ICityService _service;
        public CityController(ICityService service)
        {
            _service = service;
        }
        public async Task<IActionResult> CityTable(int page = 1, int take = 20)
        {
            return View(await _service.GetAllAsync(page, take));
        }
        public async Task<IActionResult> Create()
        { 
            return View(await _service.CreateGet(new CityCreateVM()));
        }
        [HttpPost]
        public async Task<IActionResult> Create(CityCreateVM cityCreateVM)
        {
            if(!await _service.Create(cityCreateVM, ModelState))
            {
                return View(cityCreateVM);
            }
            return RedirectToAction("CityTable", "City");
        }
    }
}
