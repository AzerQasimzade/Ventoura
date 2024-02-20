using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.ViewModels.Pagination;
using Ventoura.Application.ViewModels.Tours;

namespace Ventoura.Application.Abstractions.Services
{
    public interface IReserveService
    {
        Task<bool> Create(TourReserveVM vm, ModelStateDictionary modelstate);
        Task<List<TourReserveVM>> GetAllAsyncForReserve();

        //Task<TourUpdateVM> UpdateGet(int id, TourUpdateVM vm);
        //Task<bool> Update(int id, TourUpdateVM vm, ModelStateDictionary modelstate);
    }
}
