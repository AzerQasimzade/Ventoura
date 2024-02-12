using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ventoura.Application.Abstractions.Repositories;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Wishlist;
using Ventoura.Domain.Entities;

namespace Ventoura.Persistence.Implementations.Services
{
	public class WishlistService : IWishlistService
	{
		private readonly IWishlistRepository _repository;
		private readonly IHttpContextAccessor _accessor;

		public WishlistService(IWishlistRepository repository, IHttpContextAccessor accessor)
		{
			_repository = repository;
			_accessor = accessor;
		}

		public async Task<ICollection<WishlistItemVM>> Index()
		{
			List<WishlistItemVM> items = new List<WishlistItemVM>();
			if (_accessor.HttpContext.Request.Cookies["Wishlist"] is not null)
			{
				List<WishlistCookieItemVM> cookies = JsonConvert.DeserializeObject<List<WishlistCookieItemVM>>(_accessor.HttpContext.Request.Cookies["Wishlist"]);
				foreach (var cookie in cookies)
				{

					Tour tour = await _repository.GetFirstOrDefaultAsync(c => c.Id == cookie.Id, false, nameof(Tour.TourImages));
					if (tour is not null)
					{
						WishlistItemVM item = new WishlistItemVM
						{
							Description = tour.Description,
							SalePrice = tour.SalePrice,
							Image = tour.TourImages.FirstOrDefault().Url,
							Price = tour.Price,
							Name = tour.Name,
							Count = cookie.Count
						};
						items.Add(item);
					}
				}
			}
			return items;
		}
		public async Task AddWishlist(int id)
		{
			if (id <= 0) throw new Exception("Bad request");
			Tour tour = await _repository.GetFirstOrDefaultAsync(c => c.Id == id);
			if (tour is null) throw new Exception("Not Found");
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
}
