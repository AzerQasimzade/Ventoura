using Microsoft.AspNetCore.Mvc;
using Stripe;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Review;

namespace Ventoura.UI.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _service;

        public ReviewController(IReviewService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Create(int id, ReviewCreateVm vm)
        {
            if (await _service.CreateAsync(id, vm, ModelState))
            {
                ModelState.Clear();
            }
            return View("~/Views/Tour/About.cshtml");
        }
    }
}
