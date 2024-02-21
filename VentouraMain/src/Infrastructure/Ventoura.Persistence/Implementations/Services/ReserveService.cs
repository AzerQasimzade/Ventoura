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

        public ReserveService(IReserveRepository repository)
        {
            _repository = repository;
        }

        public async Task<TourGetVM> CreateGet(TourGetVM vm)
        {
            return vm;
        }
        public async Task<bool> Create(TourGetVM dto, ModelStateDictionary modelstate)
        {
            if (!modelstate.IsValid)
            {
                return false;
            }
            UserReservationInfo tour = new UserReservationInfo
            {
                Capacity = dto.Capacity,
                MemberCount = dto.MemberCount,
                Email = dto.Email, 
                Name = dto.Name,
              
            };
            await _repository.AddAsync(tour);
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
                    Id = tour.Id,
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
