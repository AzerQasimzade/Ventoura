﻿using Microsoft.AspNetCore.Mvc;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Domain.Entities;
using Ventoura.Domain.Exceptions;

namespace Ventoura.UI.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _service;
        private readonly IHttpContextAccessor _accessor;
        public BasketController(IBasketService service, IHttpContextAccessor accessor)
        {
            _service = service;
            _accessor = accessor;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _service.Index());
        }
        public async Task<IActionResult> AddBasket(int id)
        {
            if (!_accessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AppUser");
            }
            else
            {
                await _service.AddBasket(id);
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> Remove(int id)
        {
            if (id == 0) throw new WrongRequestException("Invalid request. Please provide a valid request");
            await _service.Remove(id);
            return RedirectToAction("Index", "Basket");
        }
        public async Task<IActionResult> PlusBasket(int id)
        {
            if (id == 0) return BadRequest();
            await _service.PlusBasket(id);
            return RedirectToAction("Index", "Basket");
        }
        public async Task<IActionResult> MinusBasket(int id)
        {
            if (id == 0) return BadRequest();
            await _service.MinusBasket(id);
            return RedirectToAction("Index", "Basket");
        }
        public async Task<IActionResult> CalculateTotalPrice(AdditionalOptions options)
        {
            await _service.CalculateTotalPrice(options);
            return RedirectToAction("Index", "Basket");
        }
        //public async Task<IActionResult> CheckOut()
        //{ 
        //    return View(await _service.CheckOut());  
        //}

    }
}
