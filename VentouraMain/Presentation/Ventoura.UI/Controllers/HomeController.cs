using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Ventoura.Application.Abstractions.Services;

namespace Ventoura.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITourService _tourService;

        public HomeController(ITourService tourService)
        {
            _tourService = tourService;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 20)
        {
            return View(await _tourService.GetAllAsync(page, take));
        }
        public async Task<IActionResult> Details(int id)
        {
			if (id <= 0) return BadRequest();
			var tour = await _tourService.GetByIdAsync(id);
			if (tour == null)
			{
				ModelState.AddModelError(string.Empty, "The product you are looking for is no longer available");
				return View("Error");
			}
            return View(await _tourService.GetByIdAsync(id)); 
        }

    }
}