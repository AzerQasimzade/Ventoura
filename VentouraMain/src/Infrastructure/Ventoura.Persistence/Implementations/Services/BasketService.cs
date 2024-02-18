using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.Abstractions.Repositories;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Basket;
using Ventoura.Domain.Entities;

namespace Ventoura.Persistence.Implementations.Services
{
    public class BasketService:IBasketService
    {
        private readonly IBasketRepository _repository;
        private readonly IHttpContextAccessor _accessor;
        private readonly UserManager<AppUser> _userManager;
        public BasketService(IBasketRepository repository, IHttpContextAccessor accessor, UserManager<AppUser> userManager)
        {
            _repository = repository;
            _accessor = accessor;
            _userManager = userManager;
        }
        public async Task<List<BasketItemVM>> Index()
        {
            List<BasketItemVM> items = new List<BasketItemVM>();

            if (_accessor.HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.Users
                    .Include(u=>u.BasketItems)
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
        public async Task AddBasket(int id)
        {
            if (id <= 0) throw new Exception("Bad request");
            Tour tour = await _repository.GetFirstOrDefaultAsync(c => c.Id == id);
            if (tour is null) throw new Exception("Not Found");
            if (_accessor.HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.Users
                    .Include(u => u.BasketItems)
                    .FirstOrDefaultAsync(u => u.Id == _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                BasketItem item = user.BasketItems.FirstOrDefault(bi => bi.TourId == tour.Id);
                if (item is null)
                {
                    item = new BasketItem
                    {
                        AppUserId = user.Id,
                        TourId = tour.Id,
                        Count = 1,
                        Price = tour.Price,
                    };
                    user.BasketItems.Add(item);
                }
                else
                {
                    item.Count++;
                }
                await _repository.SaveChangesAsync();
            }
            else
            {
                List<BasketCookieItemVM> basket;

                if (_accessor.HttpContext.Request.Cookies["Basket"] is null)
                {
                    basket = new List<BasketCookieItemVM>();
                    BasketCookieItemVM item = new BasketCookieItemVM
                    {
                        Id = id,
                        Count = 1,
                    };
                    basket.Add(item);
                }
                else
                {
                    basket = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(_accessor.HttpContext.Request.Cookies["Basket"]);
                    BasketCookieItemVM existed = basket.FirstOrDefault(w => w.Id == id);
                    if (existed is null)
                    {
                        BasketCookieItemVM item = new BasketCookieItemVM
                        {
                            Id = id,
                            Count = 1,
                        };
                        basket.Add(item);
                    }
                    else
                    {
                        existed.Count++;
                    }
                }
                string json = JsonConvert.SerializeObject(basket);
                _accessor.HttpContext.Response.Cookies.Append("Basket", json);
            }
        }
        public async Task Remove(int id)
        {
            Tour tour = await _repository.GetFirstOrDefaultAsync(p => p.Id == id);
            List<BasketCookieItemVM> basket;
            AppUser user = await _userManager.Users
           .Include(u => u.BasketItems.Where(o => o.OrderId == null))
           .FirstOrDefaultAsync(bi => bi.Id == _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var excisted = user.BasketItems
            .FirstOrDefault(x => x.TourId == tour.Id);
            if (excisted is null) throw new Exception("existed is null");
            user.BasketItems.Remove(excisted);
            await _repository.SaveChangesAsync();
        }
        public async Task PlusBasket(int id)
        {
            if (id <= 0) throw new Exception("Bad Request");
            Tour tour = await _repository.GetFirstOrDefaultAsync(p => p.Id == id);
            if (tour is null) throw new Exception("Not Found");
            if (_accessor.HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.Users
                    .Include(u => u.BasketItems.Where(o => o.OrderId == null)).FirstOrDefaultAsync(x => x.Id == _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (user == null) throw new Exception("Not Found");
                user.BasketItems.FirstOrDefault(x => x.TourId == tour.Id).Count++;
                _repository.SaveChangesAsync();
            }
        }
        public async Task MinusBasket(int id)
        {
            if (id <= 0) throw new Exception("Bad Request");
            Tour tour = await _repository.GetFirstOrDefaultAsync(p => p.Id == id);
            if (tour is null) throw new Exception("Not Found");
            if (_accessor.HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.Users
                    .Include(u => u.BasketItems.Where(o => o.OrderId == null)).FirstOrDefaultAsync(x => x.Id == _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (user == null) throw new Exception("Not Found");
                var excisted = user.BasketItems
                    .FirstOrDefault(x => x.TourId == tour.Id);
                user.BasketItems.FirstOrDefault(x => x.TourId == tour.Id).Count--;
                if (user.BasketItems.FirstOrDefault(x => x.TourId == tour.Id).Count == 0)
                {
                    user.BasketItems.Remove(excisted);
                }
                _repository.SaveChangesAsync();
            }
        }

    }
}

