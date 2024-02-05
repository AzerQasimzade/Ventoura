using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.Abstractions.Repositories;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Domain.Entities;
using Ventoura.Persistence.DAL;

namespace Ventoura.Persistence.Implementations.Repositories
{
    public class CountryRepository:Repository<Country>,ICountryRepository
    {
        public CountryRepository(AppDbContext context) : base(context)
        {
            
        }
    }
}
