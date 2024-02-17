using Microsoft.AspNetCore.Mvc;
using Ventoura.Application.Abstractions.Services;

namespace Ventoura.UI.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _service;
        public BasketController(IBasketService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _service.Index());
        }
        public async Task<IActionResult> AddBasket(int id)
        {
            if (id == 0) return BadRequest();
            await _service.AddBasket(id);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Remove(int id)
        {
            if (id == 0) return BadRequest();
            await _service.Remove(id);
            return RedirectToAction("Index", "Basket");
        }
        public async Task<IActionResult> PlusBasket(int id)
        {
            if (id == 0) return BadRequest();
            await _service.PlusBasket(id);
            return RedirectToAction("Index","Basket");
        }
        public async Task<IActionResult> MinusBasket(int id)
        {
            if (id == 0) return BadRequest();
            await _service.MinusBasket(id);
            return RedirectToAction("Index", "Basket");
        }

    }
}
