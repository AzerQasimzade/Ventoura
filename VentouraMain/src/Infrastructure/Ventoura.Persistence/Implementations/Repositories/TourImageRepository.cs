using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.Abstractions.Repositories;
using Ventoura.Domain.Entities;
using Ventoura.Persistence.DAL;

namespace Ventoura.Persistence.Implementations.Repositories
{
    public class TourImageRepository:Repository<TourImage>,ITourImageRepository
    {
        public TourImageRepository(AppDbContext context) : base(context)
        {

        }
    }
}
