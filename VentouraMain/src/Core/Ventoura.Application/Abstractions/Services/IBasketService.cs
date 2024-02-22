using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.ViewModels;
using Ventoura.Application.ViewModels.Basket;
using Ventoura.Application.ViewModels.Wishlist;
using Ventoura.Domain.Entities;

namespace Ventoura.Application.Abstractions.Services
{
    public interface IBasketService
    {
        Task AddBasket(int id);
        Task<List<BasketItemVM>> Index();
        Task Remove(int id);
        Task PlusBasket(int id);
        Task MinusBasket(int id);
        Task<decimal> CalculateTotalPrice(AdditionalOptions options);
        //Task<OrderVM> CheckOut();
    }
}
