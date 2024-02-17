using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.ViewModels.Cities;
using Ventoura.Application.ViewModels.Countries;
using Ventoura.Application.ViewModels.Tours;

namespace Ventoura.Application.Abstractions.Services
{
    public interface ICountryService
    {
        Task<ICollection<CountryItemVM>> GetAllAsync(int page, int take);
        Task<bool> Create(CountryCreateVM vm, ModelStateDictionary modelstate);
        Task<CountryCreateVM> CreateGet(CountryCreateVM vm);
        Task DeleteAsync(int id);
        Task<CountryUpdateVM> UpdateGet(int id, CountryUpdateVM vm);
        Task<bool> Update(int id, CountryUpdateVM vm, ModelStateDictionary modelstate);
        Task<CountryDetailVM> GetDetail(int id, CountryDetailVM vm);
        Task<CountryDetailVM> GetByIdAsync(int id);

    }
}
