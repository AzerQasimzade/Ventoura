using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ventoura.Application.Abstractions.Repositories;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels;
using Ventoura.Application.ViewModels.Cities;
using Ventoura.Application.ViewModels.Countries;
using Ventoura.Application.ViewModels.Tours;
using Ventoura.Domain.Entities;
using Ventoura.Domain.Enums;
using Ventoura.Domain.Extensions;

namespace Ventoura.Persistence.Implementations.Services
{
    public class TourService : ITourService
    {
        private readonly ITourRepository _repository;
        private readonly ICityRepository _cityRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _accessor;

        public TourService(ITourRepository repository,ICityRepository cityRepository,ICountryRepository countryRepository,IWebHostEnvironment env,IHttpContextAccessor accessor)
        {
            _repository = repository;
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
            _env = env;
            _accessor = accessor;
        }
        public async Task<ICollection<TourItemVM>> GetAllAsync(int page, int take)
        {
            ICollection<Tour> tours =await  _repository.GetAll(null,null,false,skip: (page - 1) * take, take: take,false,nameof(Tour.City),nameof(Tour.Country),nameof(Tour.TourImages))
                .ToListAsync();
            ICollection<TourItemVM> dtos = new List<TourItemVM>();
            foreach (var tour in tours)
            {
                dtos.Add(new TourItemVM
                {
                   Id = tour.Id,
                   Name = tour.Name,
                   DayCount = tour.DayCount,
                   Sale = tour.Sale,
                   SalePrice= tour.SalePrice,
                   StartDate = tour.StartDate,
                   StartTime=tour.StartTime,
                   AdultCount = tour.AdultCount,
                   ChildrenCount = tour.ChildrenCount,
                   Description = tour.Description,
                   EndTime = tour.EndTime,
                   IncludeDesc = tour.IncludeDesc,
                   Includes=tour.Includes,
                   Price = tour.Price,
                   TotalPrice = tour.TotalPrice,
				   TourImages=tour.TourImages,
                   City=tour.City,
                   Country=tour.Country 
                });
            }
			
            return dtos;
        }
        public async Task<TourCreateVM> CreateGet(TourCreateVM vm)
        { 
            vm.Cities=await _repository.GetAllCityAsync();
            vm.Countries=await _repository.GetAllCountriesAsync();
            return vm;
        }

        public async Task<bool> Create(TourCreateVM dto, ModelStateDictionary modelstate)
        {
            if (!modelstate.IsValid)
            {
                return false;
            }
            if (await _repository.IsExistAsync(p => p.Name == dto.Name))
            {
                dto.Countries = await _repository.GetAllCountriesAsync();
                dto.Cities = await _repository.GetAllCityAsync();
                modelstate.AddModelError("Name", "We have same tour name ");
                return false;
            }
            if (!await _cityRepository.IsExistAsync(c=>c.Id==dto.CityId))
            {
                dto.Countries = await _repository.GetAllCountriesAsync();
                dto.Cities = await _repository.GetAllCityAsync();
                modelstate.AddModelError("CityId", "We have not So City with that Id");
                return false;
            }
            if (!await _countryRepository.IsExistAsync(c => c.Id == dto.CountryId))
            {
                dto.Countries = await _repository.GetAllCountriesAsync();
                dto.Cities = await _repository.GetAllCityAsync();
                modelstate.AddModelError("CountryId", "We have not So Country with that Id");
                return false;
            }

            if (!dto.MainPhoto.ValidateFileType(FileHelper.Image))
            {
                dto.Countries = await _repository.GetAllCountriesAsync();
                dto.Cities = await _repository.GetAllCityAsync();
                modelstate.AddModelError("MainPhoto", $"The file type of {dto.MainPhoto.FileName} is not supported. Please upload an image file.");
                return false;
            }
            if (!dto.MainPhoto.ValidateFileSize(SizeHelper.mb))
            {
                dto.Countries = await _repository.GetAllCountriesAsync();
                dto.Cities = await _repository.GetAllCityAsync();
                modelstate.AddModelError("MainPhoto", $"The file size of {dto.MainPhoto.FileName} is too large. Please upload a file smaller than {SizeHelper.mb}.");
                return false;
            }
            //---------------HOVER PHOTO CHECKING------------------
            if (!dto.HoverPhoto.ValidateFileType(FileHelper.Image))
            {
                dto.Countries = await _repository.GetAllCountriesAsync();
                dto.Cities = await _repository.GetAllCityAsync();
                modelstate.AddModelError("HoverPhoto", $"The file type of {dto.MainPhoto.FileName} is not supported. Please upload an image file.");
                return false;
            }
            if (!dto.HoverPhoto.ValidateFileSize(SizeHelper.mb))
            {
                dto.Countries = await _repository.GetAllCountriesAsync();
                dto.Cities = await _repository.GetAllCityAsync();
                modelstate.AddModelError("HoverPhoto", $"The file size of {dto.MainPhoto.FileName} is too large. Please upload a file smaller than {SizeHelper.mb}.");
                return false;
            }
            TourImage main = new TourImage
            {
                IsPrimary = true,
                Url = await dto.MainPhoto.CreateFileAsync(_env.WebRootPath, "rev-slider-files", "assets"),
            };

            TourImage hover = new TourImage
            {
                IsPrimary = false,
                Url = await dto.HoverPhoto.CreateFileAsync(_env.WebRootPath,"rev-slider-files", "assets"),
            };
            Tour tour = new Tour
            {
                Name = dto.Name,
                DayCount = dto.DayCount,
                Sale = dto.Sale,
                SalePrice=dto.SalePrice,
                StartDate = dto.StartDate,
                StartTime = dto.StartTime,
                AdultCount = dto.AdultCount,
                ChildrenCount = dto.ChildrenCount,
                Description = dto.Description,
                EndTime = dto.EndTime,
                IncludeDesc = dto.IncludeDesc,
                Includes = dto.Includes,
                Price = dto.Price,
                TotalPrice = dto.TotalPrice,
                CountryId= dto.CountryId,
                CityId= dto.CityId,
                TourImages = new List<TourImage> { main, hover },   
            };
            foreach (IFormFile photo in dto.Photos ?? new List<IFormFile>())
            {
                if (!photo.ValidateFileType(FileHelper.Image))
                {
                    modelstate.AddModelError("", $"{photo.FileName} file's Type is not suitable, That's why creating file's Mission Failed");
                    continue;
                }
                if (!photo.ValidateFileSize(SizeHelper.gb))
                {
                    modelstate.AddModelError("", $"{photo.FileName} file's Size is not suitable, That's why creating file's Mission Failed");
                    continue;
                }

                tour.TourImages.Add(new TourImage
                {
                    IsPrimary = null,
                    Url = await photo.CreateFileAsync(_env.WebRootPath, "rev-slider-files", "assets")
                });
            }
            await _repository.SaveChangesAsync();
            return true;
        }
		public async Task<TourGetVM> GetByIdAsync(int id)
		{
			Tour tour = await _repository.GetByIdAsync(id, false, nameof(Tour.Country), nameof(Tour.City),nameof(Tour.TourImages));
			TourGetVM getVM = new TourGetVM
			{
				AdultCount = tour.AdultCount,
				StartDate = tour.StartDate,
				Sale = tour.Sale,
				SalePrice = tour.SalePrice,
				StartTime = tour.StartTime,
				ChildrenCount = tour.ChildrenCount,
				CityId = tour.CityId,
				CountryId = tour.CountryId,
				IncludeDesc = tour.IncludeDesc,
				Includes = tour.Includes,
				Name = tour.Name,
				Description = tour.Description,
				Price = tour.Price,
				TotalPrice = tour.TotalPrice,
				DayCount = tour.DayCount,
				EndTime = tour.EndTime

			};

			// tour null değilse, diğer alanları da doldurabilirsiniz
			if (tour.City != null)
			{
				getVM.City = new IncludeCityVM { Name = tour.City.Name };// City null değilse CityViewModel'i oluştur
			}
			if (tour.Country != null)
			{
				getVM.Country = new IncludeCountryVM { Name = tour.Country.Name }; // Country null değilse CountryViewModel'i oluştur
			}
			getVM.TourImages = tour.TourImages;
			return getVM;
		}

       
        

    }
}
