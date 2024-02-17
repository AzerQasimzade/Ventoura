using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.ViewModels.Cities;
using Ventoura.Application.ViewModels.Countries;
using Ventoura.Domain.Entities;

namespace Ventoura.Application.ViewModels.Tours
{
	public class AllToursVM
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
        public City City { get; set; }
        // Relations
    }

}
