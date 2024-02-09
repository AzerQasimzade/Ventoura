using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Ventoura.Application.Abstractions.Repositories;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Basket;
using Ventoura.Domain.Entities;

namespace Ventoura.Persistence.Implementations.Services
{
	public class WishlistService:IWishlistService
	{
		private readonly IWishlistRepository _repository;
		private readonly IHttpContextAccessor _accessor;

		public WishlistService(IWishlistRepository repository,IHttpContextAccessor accessor)
        {
			_repository = repository;
			_accessor = accessor;
		}
        public async Task AddWishlist(int id)
		{
			if (id <= 0)throw new Exception("Bad Request");	
			Tour tour = await _repository.GetByIdAsync(id);
			if (tour is null)throw new Exception("Bad Request");
			BasketCookieItemVM item = new BasketCookieItemVM
			{
				Id = id,
				Count = 1
			};
			string json=JsonSerializer.Serialize(item);
		  	_accessor.HttpContext.Response.Cookies.Append("Basket", json);	 
		}

		
	}
}
