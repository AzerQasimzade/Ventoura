using Microsoft.AspNetCore.Mvc;
using Ventoura.Application.Abstractions.Services;

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
    }
}
