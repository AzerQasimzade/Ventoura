using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Tours;

namespace Ventoura.UI.Areas.VentouraAdmin.Controllers
{
    [Area("VentouraAdmin")]
    public class ReservationController : Controller
    {
        private readonly IReserveService _service;

        public ReservationController(IReserveService service)
        {
            _service = service;
        }
        public async Task<IActionResult> TourReserveList()
        {
            return View(await _service.GetAllAsyncForReserve());
        }
        public async Task<IActionResult> Create(TourReserveVM tourReserveVM)
        {
            if (!await _service.Create(tourReserveVM, ModelState)) return View(tourReserveVM);
            return RedirectToAction("Index", "Home");
        }

    }
}
