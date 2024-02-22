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
        public async Task<IActionResult> Create(int id)
        {
            return View(await _service.CreateGet(id,new TourReserveVM()));
        }
        [HttpPost]
        public async Task<IActionResult> Create(int id,TourReserveVM tourReserveVM)
        {
            if (!await _service.Create(id,tourReserveVM, ModelState))
            {
                return View(await _service.CreateGet(id, tourReserveVM));
            }
            return RedirectToAction("Index", "Home");
        }
       
    }
}
