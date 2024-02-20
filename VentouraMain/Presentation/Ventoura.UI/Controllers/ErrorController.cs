using Microsoft.AspNetCore.Mvc;

namespace Ventoura.UI.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ErrorPage(string error)
        {
            return View();
        }
    }
}
