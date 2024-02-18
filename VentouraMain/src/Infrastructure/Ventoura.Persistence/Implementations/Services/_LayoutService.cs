using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Ventoura.Application.ViewModels.Basket;
using Ventoura.Domain.Entities;
using Ventoura.Persistence.DAL;

namespace Ventoura.Persistence.Implementations.Services
{
    public class _LayoutService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly UserManager<AppUser> _userManager;

        public _LayoutService(AppDbContext context,IHttpContextAccessor accessor,UserManager<AppUser> userManager)
        {
            _context = context;
            _accessor = accessor;
            _userManager = userManager;
        }
        public async Task<Dictionary<string, string>> GetSettingsAsync()
        {
            Dictionary<string, string> settings = await _context.Settings.ToDictionaryAsync(s => s.Key, s => s.Value);
            return settings;

        }
        public async Task<List<BasketItemVM>> GetBasketAsync()
        {
            List<BasketItemVM> items = new List<BasketItemVM>();

            if (_accessor.HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.Users
                    .Include(u => u.BasketItems)
                    .ThenInclude(bi => bi.Tour)
                    .ThenInclude(p => p.TourImages.Where(pi => pi.IsPrimary == true))
                    .FirstOrDefaultAsync(u => u.Id == _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

                foreach (BasketItem wishlistItem in user.BasketItems)
                {
                    items.Add(new BasketItemVM
                    {
                        Id = wishlistItem.TourId,
                        Price = wishlistItem.Tour.Price,
                        Count = wishlistItem.Count,
                        Name = wishlistItem.Tour.Name,
                        Sale = wishlistItem.Tour.Sale,
                        Image = wishlistItem.Tour.TourImages.FirstOrDefault()?.Url
                    });
                }
            }
            return items;
        }
    }
}
