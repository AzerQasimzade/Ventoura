using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.Abstractions.Repositories;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Tours;
using Ventoura.Domain.Entities;

namespace Ventoura.Persistence.Implementations.Services
{
    public class TourService : ITourService
    {
        private readonly ITourRepository _repository;

        public TourService(ITourRepository repository)
        {
            _repository = repository;
        }
        public async Task<ICollection<TourItemVM>> GetAllAsync(int page, int take)
        {
            ICollection<Tour> tours =await  _repository.GetAll(skip: (page - 1) * take, take: take).ToListAsync();
            ICollection<TourItemVM> dtos = new List<TourItemVM>();
            foreach (var tour in tours)
            {
                dtos.Add(new TourItemVM
                {
                   Id = tour.Id,
                   Name = tour.Name,
                   DayCount = tour.DayCount,
                   Sale = tour.Sale,
                   SalePrice= tour.SalePrice,
                   StartDate = tour.StartDate,
                   StartTime=tour.StartTime,
                   AdultCount = tour.AdultCount,
                   ChildrenCount = tour.ChildrenCount,
                   Description = tour.Description,
                   EndTime = tour.EndTime,
                   IncludeDesc = tour.IncludeDesc,
                   Includes=tour.Includes,
                   Price = tour.Price,
                   TotalPrice = tour.TotalPrice,
                });
            }
            return dtos;
        }
        public async Task Create(TourCreateVM dto)
        {
            await _repository.AddAsync(new Tour
            {
                Name = dto.Name,
                DayCount = dto.DayCount,
                Sale = dto.Sale,
                SalePrice = dto.SalePrice,
                StartDate = dto.StartDate,
                StartTime = dto.StartTime,
                AdultCount = dto.AdultCount,
                ChildrenCount = dto.ChildrenCount,
                Description = dto.Description,
                EndTime = dto.EndTime,
                IncludeDesc = dto.IncludeDesc,
                Includes = dto.Includes,
                Price = dto.Price,
                TotalPrice = dto.TotalPrice
            });
            await _repository.SaveChangesAsync();
        }

        
    }
}
