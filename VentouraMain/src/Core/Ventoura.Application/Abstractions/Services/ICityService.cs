using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.ViewModels.Cities;
using Ventoura.Application.ViewModels.Tours;
using Ventoura.Domain.Entities;

namespace Ventoura.Application.Abstractions.Services
{
    public interface ICityService
    {
        Task<ICollection<CityItemVM>> GetAllAsync(int page, int take);
        Task<bool> Create(CityCreateVM vm, ModelStateDictionary modelstate);
        Task<CityCreateVM> CreateGet(CityCreateVM vm);
        Task DeleteAsync(int id);
        Task<CityUpdateVM> UpdateGet(int id, CityUpdateVM vm);
        Task<bool> Update(int id, CityUpdateVM vm, ModelStateDictionary modelstate);
        Task<CityDetailVM> GetDetail(int id, CityDetailVM vm);
	}
}
