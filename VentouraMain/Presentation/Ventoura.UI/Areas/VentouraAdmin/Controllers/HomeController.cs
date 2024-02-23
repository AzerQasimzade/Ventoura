using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Tours;
using Ventoura.Domain.Entities;
using Ventoura.Persistence.DAL;
using Ventoura.Persistence.Implementations.Services;

namespace Ventoura.UI.Areas.VentouraAdmin.Controllers
{
    [Area("VentouraAdmin")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            List<Tour> vm=await _context.Tours.ToListAsync();
            TourGetVM getVM = new TourGetVM
            {
                Capacity = vm.Capacity,

            };
            return View(getVM);
        } 
    }

}
