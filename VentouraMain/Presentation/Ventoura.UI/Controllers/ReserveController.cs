using Microsoft.AspNetCore.Mvc;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Tours;

namespace Ventoura.UI.Controllers
{
    public class ReserveController : Controller
    {
        private readonly IReserveService _service;

        public ReserveController(IReserveService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Create()
        {
            return View(await _service.CreateGet(new TourGetVM()));
        }
        [HttpPost]
        public async Task<IActionResult> Create(TourGetVM tourReserveVM)
        {
            if (!await _service.Create(tourReserveVM, ModelState)) return View(tourReserveVM);
            return RedirectToAction("Index", "Home");
        }

    }
}
