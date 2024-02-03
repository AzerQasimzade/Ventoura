using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
namespace Ventoura.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}