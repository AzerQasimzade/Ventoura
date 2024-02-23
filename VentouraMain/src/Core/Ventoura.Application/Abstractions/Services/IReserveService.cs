using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.Abstractions.Repositories;
using Ventoura.Application.ViewModels.Pagination;
using Ventoura.Application.ViewModels.Tours;
using Ventoura.Domain.Entities;
using Ventoura.Domain.Exceptions;

namespace Ventoura.Application.Abstractions.Services
{
    public interface IReserveService
    {
        Task<TourReserveVM> CreateGet(int id,TourReserveVM vm);
        Task<bool> Create(int id,TourReserveVM vm, ModelStateDictionary modelstate);
        Task<List<TourReserveVM>> GetAllAsyncForReserve();
        Task<TourReserveVM> GetReservationByIdAsync(int reservationId);
        Task<UserReservationInfo> FindAsync(int reservationId);
        Task DeleteReservationAsync(int reservationId);
        //Task UpdateReservationStatusAsync(int reservationId, string status);

        //Task UpdateReservationStatusAndRemoveFromListAsync(int reservationId, string status);
        

        //Task<TourUpdateVM> UpdateGet(int id, TourUpdateVM vm);
        //Task<bool> Update(int id, TourUpdateVM vm, ModelStateDictionary modelstate);
    }
}
