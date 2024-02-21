﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
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
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _repository;

        public CountryService(ICountryRepository repository)
        {
            _repository = repository;
        }
        public async Task<ICollection<CountryItemVM>> GetAllAsync(int page, int take)
        {
            ICollection<Country> countries = await _repository.GetAll(skip: (page - 1) * take, take: take).ToListAsync();
            ICollection<CountryItemVM> dtos = new List<CountryItemVM>();
            foreach (var country in countries)
            {
                dtos.Add(new CountryItemVM
                {
                   Id = country.Id,
                   Name = country.Name,
                });
            }
            return dtos;
        }

        public async Task<CountryCreateVM> CreateGet(CountryCreateVM vm)
        {
            return vm;
        }
        public async Task<bool> Create(CountryCreateVM vm, ModelStateDictionary modelstate)
        {
            if (!modelstate.IsValid)
            {
                return false;
            }
            await _repository.AddAsync(new Country
            {
                Name = vm.Name,
            });
            await _repository.SaveChangesAsync();
            return true;
        }
        public async Task DeleteAsync(int id)
        {
            Country existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new NotFoundException("Country cannot found. Please check the URL and try again");
            _repository.Delete(existed);
            await _repository.SaveChangesAsync();
        }

        public async Task<CountryUpdateVM> UpdateGet(int id, CountryUpdateVM vm)
        {
            Country country=await _repository.GetByIdAsync(id);
            CountryUpdateVM countryUpdateVM = new CountryUpdateVM
            {
                Name = country.Name,
            };
            return countryUpdateVM;
        }
        public async Task<bool> Update(int id, CountryUpdateVM vm, ModelStateDictionary modelstate)
        {
            Country existed=await _repository.GetByIdAsync(id);
            if (!modelstate.IsValid)
            {
                return false;
            }
            existed.Name = vm.Name;
            _repository.Update(existed);
            await _repository.SaveChangesAsync();
            return true;
        }
        public async Task<CountryDetailVM> GetDetail(int id, CountryDetailVM vm)
        {
            Country country = await _repository.GetByIdAsync(id);
            CountryDetailVM getVM = new CountryDetailVM
            {
                Name = country.Name,
            };
            return getVM;
        }
        public async Task<CountryDetailVM> GetByIdAsync(int id)
        {
            Country country = await _repository.GetByIdAsync(id);
            CountryDetailVM getVM = new CountryDetailVM
            {
               Name= country.Name,
            };
            return getVM;
        }
    }
}
