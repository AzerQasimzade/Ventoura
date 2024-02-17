using Microsoft.AspNetCore.Mvc;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Domain.Entities;

namespace Ventoura.UI.Controllers
{
	public class WishListController : Controller
	{
		private readonly IWishlistService _service;
		public WishListController(IWishlistService service)
        {
			_service = service;
		}
        public async Task<IActionResult> Index()
		{	
			return View(await _service.Index());
		}
		public async Task<IActionResult> AddWishlist(int id)
		{
			if (id == 0) return BadRequest();
			await _service.AddWishlist(id);
			return RedirectToAction("Index", "Home");
		}
        public async Task<IActionResult> Remove(int id)
        {
            if (id == 0) return BadRequest();
            await _service.Remove(id);
            return RedirectToAction("Index", "Wishlist");
        }

    }
}
