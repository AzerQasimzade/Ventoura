using Microsoft.AspNetCore.Mvc.ModelBinding;
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
using Ventoura.Application.ViewModels.Tours;
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
        public async  Task<CityCreateVM> CreateGet(CityCreateVM vm)
        {
            return vm;
        }
        public async Task<bool> Create(CityCreateVM vm, ModelStateDictionary modelstate)
        {
            if (!modelstate.IsValid)
            {
                return false;
            }
            await _repository.AddAsync(new City
            {
                Name = vm.Name,
            });
            await _repository.SaveChangesAsync();
            return true;
        }   
    }
}
