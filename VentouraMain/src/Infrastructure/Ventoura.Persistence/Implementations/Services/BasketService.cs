﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.Abstractions.Repositories;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Basket;
using Ventoura.Application.ViewModels.Wishlist;
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
        public async Task<ICollection<BasketItemVM>> Index()
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
                        SalePrice = wishlistItem.Tour.SalePrice,
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
                    .Include(u => u.WishlistItems)
                    .FirstOrDefaultAsync(u => u.Id == _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                BasketItem item = user.BasketItems.FirstOrDefault(bi => bi.TourId == tour.Id);
                if (item is null)
                {
                    //item = new BasketItem
                    //{
                    //    AppUserId = user.Id,
                    //    TourId = tour.Id,
                    //    Count = 1,
                    //    Price = tour.Price,
                    //};
                    //user.WishlistItems.Add(item);
                }
                else
                {
                    item.Count++;
                }
                await _repository.SaveChangesAsync();
            }
            else
            {
                List<WishlistCookieItemVM> wishlist;

                if (_accessor.HttpContext.Request.Cookies["Wishlist"] is null)
                {
                    wishlist = new List<WishlistCookieItemVM>();
                    WishlistCookieItemVM item = new WishlistCookieItemVM
                    {
                        Id = id,
                        Count = 1,
                    };
                    wishlist.Add(item);
                }
                else
                {
                    wishlist = JsonConvert.DeserializeObject<List<WishlistCookieItemVM>>(_accessor.HttpContext.Request.Cookies["Wishlist"]);
                    WishlistCookieItemVM existed = wishlist.FirstOrDefault(w => w.Id == id);
                    if (existed is null)
                    {
                        WishlistCookieItemVM item = new WishlistCookieItemVM
                        {
                            Id = id,
                            Count = 1,
                        };
                        wishlist.Add(item);
                    }
                    else
                    {
                        existed.Count++;
                    }
                }
                string json = JsonConvert.SerializeObject(wishlist);
                _accessor.HttpContext.Response.Cookies.Append("Wishlist", json);
            }
        }
        public async Task Remove(int id)
        {
            Tour tour = await _repository.GetFirstOrDefaultAsync(p => p.Id == id);
            List<WishlistCookieItemVM> wishlist;
            AppUser user = await _userManager.Users
           .Include(u => u.WishlistItems.Where(o => o.OrderId == null))
           .FirstOrDefaultAsync(bi => bi.Id == _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var excisted = user.WishlistItems
            .FirstOrDefault(x => x.TourId == tour.Id);
            if (excisted is null) throw new Exception("existed is null");

            user.WishlistItems.Remove(excisted);
            await _repository.SaveChangesAsync();

        }
    }
}
