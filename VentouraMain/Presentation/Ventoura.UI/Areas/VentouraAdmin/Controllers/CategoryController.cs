using Microsoft.AspNetCore.Mvc;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels;
using Ventoura.Application.ViewModels.Cities;
using Ventoura.Domain.Exceptions;

namespace Ventoura.UI.Areas.VentouraAdmin.Controllers
{
	[Area("VentouraAdmin")]   

	public class CategoryController : Controller
	{
		private readonly ICategoryService _service;
		public CategoryController(ICategoryService service)
		{
			_service = service;
		}
		public async Task<IActionResult> CategoryTable(int page = 1, int take = 20)
		{
			if (page<=0 && take<=0)throw new WrongRequestException("Bad request. Please provide a valid request"); 
			return View(await _service.GetAllAsync(page, take));
		}
		public async Task<IActionResult> Create()
		{
			return View(await _service.CreateGet(new CategoryCreateVM()));
		}
		[HttpPost]
		public async Task<IActionResult> Create(CategoryCreateVM categoryCreateVM)
		{
			if (!await _service.Create(categoryCreateVM, ModelState))
			{
				return View(categoryCreateVM);
			}
			return RedirectToAction("CategoryTable", "Category");
		}
		public async Task<IActionResult> Delete(int id)
		{
			if (id<=0) throw new WrongRequestException("Bad request. Please provide a valid request");
            await _service.DeleteAsync(id);
			return RedirectToAction("CategoryTable", "Category");
		}
		public async Task<IActionResult> Update(int id)
		{
			if (id <= 0) throw new WrongRequestException("Bad request. Please provide a valid request");
            return View(await _service.UpdateGet(id, new CategoryUpdateVM()));
		}
		[HttpPost]
		public async Task<IActionResult> Update(int id, CategoryUpdateVM vm)
		{
			if (id <= 0) throw new WrongRequestException("Bad request. Please provide a valid request");
            if (!await _service.Update(id, vm, ModelState)) return View(vm);
			return RedirectToAction("CategoryTable", "City");
		}
	}
}
