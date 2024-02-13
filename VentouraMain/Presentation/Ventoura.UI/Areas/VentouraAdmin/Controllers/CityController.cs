using Microsoft.AspNetCore.Mvc;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Cities;

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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CityCreateVM cityCreateVM)
        {
            return View();
        }
    }
}
