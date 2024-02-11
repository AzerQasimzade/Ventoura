using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.ViewModels.Wishlist;
using Ventoura.Domain.Entities;

namespace Ventoura.Application.Abstractions.Services
{
	public interface IWishlistService
	{
		Task AddWishlist(int id);
		Task<ICollection<WishlistItemVM>> Index();
	}
}
