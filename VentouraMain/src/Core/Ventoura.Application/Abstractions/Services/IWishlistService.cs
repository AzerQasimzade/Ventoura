using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.ViewModels.Basket;

namespace Ventoura.Application.Abstractions.Services
{
	public interface IWishlistService
	{
		Task AddWishlist(int id);
	}
}
