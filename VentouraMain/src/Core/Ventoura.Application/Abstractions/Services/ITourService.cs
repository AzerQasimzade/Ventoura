using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.ViewModels;
using Ventoura.Application.ViewModels.Tours;

namespace Ventoura.Application.Abstractions.Services
{
    public interface ITourService
    {
        Task<ICollection<TourItemVM>> GetAllAsync(int page, int take);
        Task<TourGetVM> GetByIdAsync(int id);

        Task<bool> Create(TourCreateVM vm,ModelStateDictionary modelstate);
        Task<TourCreateVM> CreateGet(TourCreateVM vm);
        Task<TourUpdateVM> UpdateGet(int id,TourUpdateVM vm);
        Task<bool> Update(int id, TourUpdateVM tourUpdateDto);
        //Task DeleteAsync(int id);
    }
}
