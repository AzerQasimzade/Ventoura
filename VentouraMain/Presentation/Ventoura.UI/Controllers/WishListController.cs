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
        public IActionResult Index()
		{
			return View();
		}
		public IActionResult AddWishlist(int id)
		{
			if (id == 0) return BadRequest();
			return View(_service.AddWishlist(id));	
		}
		public IActionResult GetWishList()
		{
			return Content(Request.Cookies["Basket"]);
		}
	}
}
