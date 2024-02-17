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
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction("CityTable", "City");
        }

        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            return View(await _service.UpdateGet(id, new CityUpdateVM()));
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, CityUpdateVM vm)
        {
            if (id <= 0) return BadRequest();
            if (!await _service.Update(id, vm, ModelState)) return View(vm);
            return RedirectToAction("CityTable", "City");
        }
    }
}
