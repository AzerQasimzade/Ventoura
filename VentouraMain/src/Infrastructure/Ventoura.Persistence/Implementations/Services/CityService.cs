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
using Ventoura.Domain.Exceptions;

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
        public async Task<CityCreateVM> CreateGet(CityCreateVM vm)
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
        public async Task DeleteAsync(int id)
        {
            City existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException("City cannot found. Please check the URL and try again");
            _repository.Delete(existed);
            await _repository.SaveChangesAsync();
        }
        public async Task<CityUpdateVM> UpdateGet(int id, CityUpdateVM vm)
        {
            City city = await _repository.GetByIdAsync(id);
            CityUpdateVM cityUpdateVM = new CityUpdateVM
            {
                Name = city.Name,
            };
            return cityUpdateVM;
        }
        public async Task<bool> Update(int id, CityUpdateVM vm, ModelStateDictionary modelstate)
        {
            City existed = await _repository.GetByIdAsync(id);
            if (!modelstate.IsValid)
            {
                return false;
            }
            existed.Name = vm.Name;
            _repository.Update(existed);
            await _repository.SaveChangesAsync();
            return true;
        }
        public async Task<CityDetailVM> GetDetail(int id, CityDetailVM vM)
        {
            City city = await _repository.GetByIdAsync(id);
            CityDetailVM getVM = new CityDetailVM
            {
                Name = city.Name,
            };
            return getVM;
        }		

	}
}