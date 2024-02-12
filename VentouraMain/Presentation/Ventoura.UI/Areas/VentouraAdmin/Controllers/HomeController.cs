using Microsoft.AspNetCore.Mvc;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Tours;
using Ventoura.Persistence.Implementations.Services;

namespace Ventoura.UI.Areas.VentouraAdmin.Controllers
{
    [Area("VentouraAdmin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        } 
    }

}
