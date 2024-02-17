using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Tours;
using Ventoura.Domain.Entities;
using Ventoura.Persistence.Implementations.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public async Task<IActionResult> Create() 
        {
            return View(await _service.CreateGet(new TourCreateVM())); 
        }
        [HttpPost]
        public async Task<IActionResult> Create(TourCreateVM tourCreateVM)
        {
            if (!await _service.Create(tourCreateVM, ModelState))return View(tourCreateVM); 
            return RedirectToAction("TourTable","Tour");
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0)return BadRequest(); 
            return View(await _service.UpdateGet(id,new TourUpdateVM()));
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, TourUpdateVM vm)
        {
            if (id <= 0) return BadRequest();
            if (!await _service.Update(id, vm, ModelState)) return View(vm);
            return RedirectToAction("TourTable", "Tour");         
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction("TourTable","Tour");
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
            return View(await _service.GetDetail(id, new TourDetailVM()));
        }
    }
}
