using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventoura.Application.ViewModels.Wishlist
{
	public class WishlistItemVM
	{
        public int Id { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
