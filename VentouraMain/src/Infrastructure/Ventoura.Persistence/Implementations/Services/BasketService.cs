using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.Abstractions.Repositories;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels;
using Ventoura.Application.ViewModels.Basket;
using Ventoura.Application.ViewModels.Wishlist;
using Ventoura.Domain.Entities;
using Ventoura.Domain.Exceptions;

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
						Subtotal = wishlistItem.Count * wishlistItem.Tour.Price,	
						SubtotalforDiscount = wishlistItem.Count*(wishlistItem.Tour.Price-((wishlistItem.Tour.Price*wishlistItem.Tour.Sale)/100)),
						Image = wishlistItem.Tour.TourImages.FirstOrDefault()?.Url
                    });
                }
                
            }
            return items;
        }
        public async Task AddBasket(int id)
        {
            if (id <= 0) throw new WrongRequestException("Bad request. Please provide a valid request");
            Tour tour = await _repository.GetFirstOrDefaultAsync(c => c.Id == id);
            if (tour is null) throw new NotFoundException("Page not found. Please check the URL and try again");

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
            if (id <= 0) throw new WrongRequestException("Bad request. Please provide a valid request");
            Tour tour = await _repository.GetFirstOrDefaultAsync(p => p.Id == id);
            if (tour is null) throw new NotFoundException("Page not found. Please check the URL and try again");
            if (_accessor.HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.Users
                    .Include(u => u.BasketItems.Where(o => o.OrderId == null)).FirstOrDefaultAsync(x => x.Id == _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (user == null) throw new NotFoundException("Page not found. Please check the URL and try again");
                user.BasketItems.FirstOrDefault(x => x.TourId == tour.Id).Count++;
                _repository.SaveChangesAsync();
            }
        }
        public async Task MinusBasket(int id)
        {
            if (id <= 0) throw new WrongRequestException("Bad request. Please provide a valid request");
            Tour tour = await _repository.GetFirstOrDefaultAsync(p => p.Id == id);
            if (tour is null) throw new NotFoundException("Page not found. Please check the URL and try again");
            if (_accessor.HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.Users
                    .Include(u => u.BasketItems.Where(o => o.OrderId == null)).FirstOrDefaultAsync(x => x.Id == _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (user == null) throw new NotFoundException("Page not found. Please check the URL and try again");
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
        public async Task<decimal> CalculateTotalPrice(AdditionalOptions options)
        {
            decimal totalPrice = 0;

            if (options.DedicatedTourGuide)
            {
                totalPrice += 34; // Örnek fiyat
            }
            if (options.Insurance)
            {
                totalPrice += 15; // Örnek fiyat
            }
            if (options.CoffeeBreak)
            {
                totalPrice += 10; // Örnek fiyat
            }
            return totalPrice;
        }

        //public async Task<OrderVM> CheckOut()
        //{
        //    AppUser user=await _userManager.Users
        //        .Include(u=>u.BasketItems)
        //        .ThenInclude(u=>u.Tour)
        //        .FirstOrDefaultAsync(u=>u.Id==_accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        //    OrderVM orderVM = new OrderVM
        //    {
        //        BasketItems = user.BasketItems
        //    };

        //    Order order=new Order
        //    {
        //        Status=null,
        //        Address=orderVM.Address,
        //        AppUserId=user.Id,
        //        PurchasedAt=DateTime.Now,


        //    }
        //    return orderVM;
        //}
        //public async Task<bool> CheckOut(OrderCreateVm orderVM, ModelStateDictionary modelstate, ITempDataDictionary tempdata, string stripeEmail, string stripeToken)
        //{
        //    if (_accessor.HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        AppUser user = await _autentication.GetUserAsync(_accessor.HttpContext.User.Identity.Name);

        //        if (!modelstate.IsValid)
        //        {
        //            return false;

        //        }

        //        Order order = new Order
        //        {
        //            AppUserId = user.Id,
        //            Status = OrderStatus.Pending,
        //            Address = orderVM.UserAddress,
        //            UserName = orderVM.UserName,
        //            UserSurname = orderVM.UserSurname,
        //            UserEmail = orderVM.UserEmail,
        //            UserPhone = orderVM.UserPhoneNumber,
        //            NoteForRestaurant = orderVM.NotesForRestaurant,
        //            CreatedAt = DateTime.Now,
        //            PurchasedAt = DateTime.Now,
        //            TotalPrice = 0,
        //            IsDeleted = false,
        //            OrderItems = new List<OrderItem>()
        //        };
        //        decimal total = 0;
        //        foreach (var item in user.BasketItems)
        //        {
        //            Meal meal = await _mealRepository.GetByIdAsync(item.MealId, isDeleted: false);

        //            total += item.Count * meal.Price;


        //            if (meal is not null)
        //            {
        //                order.OrderItems.Add(new OrderItem
        //                {
        //                    Count = item.Count,
        //                    Price = meal.Price,
        //                    MealName = meal.Name,
        //                    MealId = meal.Id,
        //                    OrderId = order.Id
        //                });
        //            }
        //        }
        //        ICollection<int> restaurantcount = new List<int>();
        //        if (order.OrderItems.Count != 0)
        //        {
        //            for (int i = 0; i < order.OrderItems.Count; i++)
        //            {
        //                Meal meal = await _mealRepository.GetByIdAsync(order.OrderItems[i].MealId, isDeleted: false);
        //                if (meal == null) throw new Exception("Meal not Found");
        //                if (!restaurantcount.Any(x => x == meal.RestaurantId))
        //                {
        //                    restaurantcount.Add(meal.RestaurantId);
        //                }
        //            }
        //        }
        //        total = restaurantcount.Count * 10 + total;
        //        var optionCust = new CustomerCreateOptions
        //        {
        //            Email = stripeEmail,
        //            Name = user.Name + " " + user.Surname,
        //            Phone = order.UserPhone
        //        };
        //        var serviceCust = new CustomerService();
        //        Customer customer = serviceCust.Create(optionCust);

        //        total = total * 100;
        //        var optionsCharge = new ChargeCreateOptions
        //        {

        //            Amount = (long)total,
        //            Currency = "USD",
        //            Description = "Product Selling amount",
        //            Source = stripeToken,
        //            ReceiptEmail = stripeEmail


        //        };
        //        var serviceCharge = new ChargeService();
        //        Charge charge = serviceCharge.Create(optionsCharge);

        //        if (charge.Status != "succeeded")
        //        {
        //            modelstate.AddModelError("Address", "Odenishde problem var");
        //            return false;
        //        }


        //        order.TotalPrice = total;
        //        await _repository.AddAsync(order);
        //        user.BasketItems = new List<BasketItem>();
        //        await _repository.SaveChangesAsync();
        //        _accessor.HttpContext.Session.SetInt32("OrderId", order.Id);
        //        //tempdata["OrderId"]=order.Id;
        //    }
        //    return true;
        //}
    }
}

