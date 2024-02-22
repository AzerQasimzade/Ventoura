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
                    StartDate=tour.StartDate,
                    Capacity = tour.Capacity,
                    Email = tour.Email,
                    MemberCount= tour.MemberCount,
                    Name = tour.Name,    
                });
            } 
            return dtos;
        }
    }
}
