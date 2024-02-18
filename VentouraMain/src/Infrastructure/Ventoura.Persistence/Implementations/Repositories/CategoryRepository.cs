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
	public class CategoryRepository:Repository<Category>, ICategoryRepository
	{
        public CategoryRepository(AppDbContext context):base(context)
        {
            
        }
    }
}
