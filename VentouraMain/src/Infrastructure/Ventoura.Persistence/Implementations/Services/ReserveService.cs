using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.Abstractions.Repositories;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Pagination;
using Ventoura.Application.ViewModels.Tours;
using Ventoura.Domain.Entities;
using Ventoura.Domain.Enums;
using Ventoura.Domain.Exceptions;

namespace Ventoura.Persistence.Implementations.Services
{
    public class ReserveService : IReserveService
    {
        private readonly IReserveRepository _repository;
        private readonly ITourRepository _tourRepository;

        public ReserveService(IReserveRepository repository,ITourRepository tourRepository)
        {
            _repository = repository;
            _tourRepository = tourRepository;
        }
        public async Task<TourReserveVM> CreateGet(int id,TourReserveVM vm)
        {
            Tour tour = await _tourRepository.GetByIdAsync(id);
            vm.Tour = tour;
            vm.TourId=tour.Id;
            vm.Capacity = tour.Capacity;
            vm.Name=tour.Name;
            vm.StartDate = tour.StartDate;             
            return vm;
        }
        public async Task<bool> Create(int id,TourReserveVM dto, ModelStateDictionary modelstate)
        {
            if (!modelstate.IsValid)
            {
                return false;
            }
            Tour tour = await _tourRepository.GetByIdAsync(id);
            UserReservationInfo tourinfo = new UserReservationInfo
            {
                TourId= tour.Id,
                StartDate = dto.StartDate,
                Capacity = tour.Capacity,
                MemberCount = dto.MemberCount,
                Email = dto.Email, 
                Name = tour.Name      
            };
            await _repository.AddAsync(tourinfo);
            await _repository.SaveChangesAsync();
            return true;
        }
        public async Task<List<TourReserveVM>> GetAllAsyncForReserve()
        { 
            List<UserReservationInfo> tours = await _repository
                .GetAll(null, null, false,0,0, false)
                .ToListAsync();
            List<TourReserveVM> dtos = new List<TourReserveVM>();
            foreach (var tour in tours)
            {
                dtos.Add(new TourReserveVM
                {
                    Id=tour.Id,
                    StartDate=tour.StartDate,
                    Capacity = tour.Capacity,
                    Email = tour.Email,
                    MemberCount= tour.MemberCount,
                    Name = tour.Name,    
                });
            } 
            return dtos;
        }
        public async Task<TourReserveVM> GetReservationByIdAsync(int reservationId)
        {
           UserReservationInfo info=await _repository.GetByIdAsync(reservationId,false,nameof(UserReservationInfo.Tour));

            TourReserveVM vm = new TourReserveVM
            {
                Id = info.Id,
                Capacity = info.Capacity,
                StartDate = info.StartDate,
                Email = info.Email,
                MemberCount = info.MemberCount,
                Name = info.Name,
                Price=info.Tour.Price
            };
            return vm;
        }
        public async Task<UserReservationInfo> FindAsync(int reservationId)
        {
           return  await _repository.FindAsync(reservationId);
        }

        public async Task DeleteReservationAsync(int reservationId)
        {
            _repository.DeleteAsync(reservationId);
            await _repository.SaveChangesAsync();
        }
        //public async Task UpdateReservationStatusAsync(int reservationId, string status)
        //{
        //    var reservation = await _repository.GetReservationByIdAsync(reservationId);

        //    if (reservation == null)
        //    {
        //        throw new NotFoundException($"Reservation with id {reservationId} not found.");
        //    }

        //    reservation.Status = status;

        //    await _repository.SaveChangesAsync();
        //}

        //public async Task UpdateReservationStatusAndRemoveFromListAsync(int reservationId, string status)
        //{
        //    await UpdateReservationStatusAsync(reservationId, status);

        //    //UserReservationInfo reservationToRemove = await _repository.GetReservationByIdAsync(reservationId);
        //    //if (reservationToRemove != null)
        //    //{
        //    //    _repository.RemoveReservation(reservationToRemove);
        //    //    await _repository.SaveChangesAsync();
        //    //}
        //}


    }
}
