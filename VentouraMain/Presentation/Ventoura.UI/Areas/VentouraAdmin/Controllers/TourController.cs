using Microsoft.AspNetCore.Mvc;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Tours;

namespace Ventoura.UI.Areas.VentouraAdmin.Controllers
{
    [Area("VentouraAdmin")]
    public class TourController : Controller
    {
        private readonly ITourService _service;
        public TourController(ITourService service)
        {
            _service = service;
        }
        public async Task<IActionResult> TourTable(int page = 1, int take = 20)
        {
            return View(await _service.GetAllAsync(page, take));
        }
        public IActionResult Create() 
        {
            return View(_service.CreateGet(new TourCreateVM())); 
        }
        [HttpPost]
        public async Task<IActionResult> Create(TourCreateVM tourCreateVM)
        {
            if (!await _service.Create(tourCreateVM, ModelState))
            {
                return View(tourCreateVM);
            }
            return RedirectToAction("TourTable","Tour");
        }

    }
}
