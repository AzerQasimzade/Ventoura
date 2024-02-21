using Microsoft.AspNetCore.Mvc;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Cities;
using Ventoura.Application.ViewModels.Tours;
using Ventoura.Domain.Exceptions;

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
            if (page <= 0 && take <= 0) throw new WrongRequestException("Bad request. Please provide a valid request");
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
            if(id<=0) throw new WrongRequestException("Bad request. Please provide a valid request");
            await _service.DeleteAsync(id);
            return RedirectToAction("CityTable", "City");
        }

        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) throw new WrongRequestException("Bad request. Please provide a valid request");
            return View(await _service.UpdateGet(id, new CityUpdateVM()));
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, CityUpdateVM vm)
        {
            if (id <= 0) throw new WrongRequestException("Bad request. Please provide a valid request");
            if (!await _service.Update(id, vm, ModelState)) return View(vm);
            return RedirectToAction("CityTable", "City");
        }
    }
}
