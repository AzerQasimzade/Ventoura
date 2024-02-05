using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.ViewModels.Countries;
using Ventoura.Application.ViewModels.Tours;

namespace Ventoura.Application.Abstractions.Services
{
    public interface ICountryService
    {
        Task<ICollection<CountryItemVM>> GetAllAsync(int page, int take);
        Task Create(CountryCreateVM dto);

    }
}
