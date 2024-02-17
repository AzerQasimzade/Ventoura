using Microsoft.AspNetCore.Mvc;

namespace Ventoura.UI.Controllers
{
    public class TourController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
		
	}
}
