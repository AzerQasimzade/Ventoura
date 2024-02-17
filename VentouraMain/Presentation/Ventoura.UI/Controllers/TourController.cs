using Microsoft.AspNetCore.Mvc;
using Ventoura.Application.Abstractions.Services;

namespace Ventoura.UI.Controllers
{
    public class TourController : Controller
    {
        private readonly ITourService _service;

        public TourController(ITourService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 4)
        {
            return View(await _service.GetAllAsync(page, take));
        }
        
	}
}
