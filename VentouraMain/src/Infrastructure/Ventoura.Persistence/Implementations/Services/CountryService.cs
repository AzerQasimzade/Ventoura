using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.Abstractions.Repositories;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Countries;
using Ventoura.Application.ViewModels.Tours;
using Ventoura.Domain.Entities;

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
        public async Task Create(CountryCreateVM dto)
        {
            await _repository.AddAsync(new Country
            {
                Name= dto.Name,
            });
            await _repository.SaveChangesAsync();
        }

    }
}
