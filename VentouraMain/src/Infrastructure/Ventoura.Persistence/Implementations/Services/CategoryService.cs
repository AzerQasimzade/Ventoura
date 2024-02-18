using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.Abstractions.Repositories;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels;
using Ventoura.Application.ViewModels.Cities;
using Ventoura.Domain.Entities;

namespace Ventoura.Persistence.Implementations.Services
{
	public class CategoryService:ICategoryService
	{
		private readonly ICategoryRepository _repository;

		public CategoryService(ICategoryRepository repository)
		{
			_repository = repository;
		}

		public async Task<ICollection<CategoryItemVM>> GetAllAsync(int page, int take)
		{
			ICollection<Category> cities = await _repository.GetAll(skip: (page - 1) * take, take: take).ToListAsync();
			ICollection<CategoryItemVM> dtos = new List<CategoryItemVM>();
			foreach (var category in cities)
			{
				dtos.Add(new CategoryItemVM
				{
					Id = category.Id,
					Name = category.Name,
				});
			}
			return dtos;
		}
		public async Task<CategoryCreateVM> CreateGet(CategoryCreateVM vm)
		{
			return vm;
		}
		public async Task<bool> Create(CategoryCreateVM vm, ModelStateDictionary modelstate)
		{
			if (!modelstate.IsValid)
			{
				return false;
			}
			await _repository.AddAsync(new Category
			{
				Name = vm.Name,
			});
			await _repository.SaveChangesAsync();
			return true;
		}
		public async Task DeleteAsync(int id)
		{
			Category existed = await _repository.GetByIdAsync(id);
			if (existed == null) throw new Exception("Product cant found");
			_repository.Delete(existed);
			await _repository.SaveChangesAsync();
		}
		public async Task<CategoryUpdateVM> UpdateGet(int id, CategoryUpdateVM vm)
		{
			Category category = await _repository.GetByIdAsync(id);
			CategoryUpdateVM categoryUpdateVM = new CategoryUpdateVM
			{
				Name = category.Name,
			};
			return categoryUpdateVM;
		}
		public async Task<bool> Update(int id, CategoryUpdateVM vm, ModelStateDictionary modelstate)
		{
			Category existed = await _repository.GetByIdAsync(id);
			if (!modelstate.IsValid)
			{
				return false;
			}
			existed.Name = vm.Name;
			_repository.Update(existed);
			await _repository.SaveChangesAsync();
			return true;
		}
		public async Task<CategoryDetailVM> GetDetail(int id, CategoryDetailVM vM)
		{
			Category category = await _repository.GetByIdAsync(id);
			CategoryDetailVM getVM = new CategoryDetailVM
			{
				Name = category.Name,
			};
			return getVM;
		}

	}
}
