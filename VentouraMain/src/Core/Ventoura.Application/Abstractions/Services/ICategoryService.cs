using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.ViewModels;
using Ventoura.Application.ViewModels.Cities;

namespace Ventoura.Application.Abstractions.Services
{
	public interface ICategoryService
	{
		Task<ICollection<CategoryItemVM>> GetAllAsync(int page, int take);
		Task<bool> Create(CategoryCreateVM vm, ModelStateDictionary modelstate);
		Task<CategoryCreateVM> CreateGet(CategoryCreateVM vm);
		Task DeleteAsync(int id);
		Task<CategoryUpdateVM> UpdateGet(int id, CategoryUpdateVM vm);
		Task<bool> Update(int id, CategoryUpdateVM vm, ModelStateDictionary modelstate);
		Task<CategoryDetailVM> GetDetail(int id, CategoryDetailVM vm);
	}
}
