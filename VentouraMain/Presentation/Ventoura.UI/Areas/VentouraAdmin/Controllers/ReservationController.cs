using Microsoft.AspNetCore.Mvc;

namespace Ventoura.UI.Areas.VentouraAdmin.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult TourReserveList()
        {
            return View();
        }

    }
}
