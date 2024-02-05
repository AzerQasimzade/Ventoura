using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.Abstractions.Repositories;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Cities;
using Ventoura.Application.ViewModels.Countries;
using Ventoura.Domain.Entities;

namespace Ventoura.Persistence.Implementations.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _repository;

        public CityService(ICityRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<CityItemVM>> GetAllAsync(int page, int take)
        {
            ICollection<City> cities = await _repository.GetAll(skip: (page - 1) * take, take: take).ToListAsync();
            ICollection<CityItemVM> dtos = new List<CityItemVM>();
            foreach (var city in cities)
            {
                dtos.Add(new CityItemVM
                {
                    Id = city.Id,
                    Name = city.Name,
                });
            }
            return dtos;
        }
        public async Task Create(CityCreateVM dto)
        {
            await _repository.AddAsync(new City
            {
                Name = dto.Name,
            });
            await _repository.SaveChangesAsync();
        }

    }
}
