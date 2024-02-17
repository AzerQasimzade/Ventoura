using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.ViewModels.Basket;
using Ventoura.Application.ViewModels.Wishlist;

namespace Ventoura.Application.Abstractions.Services
{
    public interface IBasketService
    {
        Task AddBasket(int id);
        Task<ICollection<BasketItemVM>> Index();
        Task Remove(int id);
        Task PlusBasket(int id);
        Task MinusBasket(int id);
    }
}
