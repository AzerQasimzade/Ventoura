﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Claims;
using Ventoura.Application.Abstractions.Repositories;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels;
using Ventoura.Application.ViewModels.Cities;
using Ventoura.Application.ViewModels.Countries;
using Ventoura.Application.ViewModels.Pagination;
using Ventoura.Application.ViewModels.Tours;
using Ventoura.Application.ViewModels.Wishlist;
using Ventoura.Domain.Entities;
using Ventoura.Domain.Enums;
using Ventoura.Domain.Extensions;

namespace Ventoura.Persistence.Implementations.Services
{
    public class TourService : ITourService
    {
        private readonly ITourRepository _repository;
        private readonly ICityRepository _cityRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IWebHostEnvironment _env;
        public TourService(ITourRepository repository, ICityRepository cityRepository,ICategoryRepository categoryRepository ,ICountryRepository countryRepository, IWebHostEnvironment env)
        {
            _repository = repository;
            _cityRepository = cityRepository;
            _categoryRepository = categoryRepository;
            _countryRepository = countryRepository;
            _env = env;
        }
        public async Task<PaginationVM<TourItemVM>> GetAllAsync(int page, int take)
        {
            int count = await _repository.GetProductCountAsync();
            if (page > Math.Ceiling((double)count / 6))
            {
                throw new Exception("Bad Request");
            }
            List<Tour> tours = await _repository
                .GetAll(null, null, false, skip: (page - 1) * take, take: take, false, nameof(Tour.City), nameof(Tour.Country),nameof(Tour.Category), nameof(Tour.TourImages))
                .ToListAsync();
            List<TourItemVM> dtos = new List<TourItemVM>();
            foreach (var tour in tours)
            {
                dtos.Add(new TourItemVM
                {
                    Id = tour.Id,
                    Name = tour.Name,
                    DayCount = tour.DayCount,
                    Sale = tour.Sale,
                    SalePrice = tour.SalePrice,
                    StartDate = tour.StartDate,
                    StartTime = tour.StartTime,
                    Description = tour.Description,
                    EndTime = tour.EndTime,
                    IncludeDesc = tour.IncludeDesc,
                    Includes = tour.Includes,
                    Price = tour.Price,
                    TotalPrice = tour.TotalPrice,
                    TourImages = tour.TourImages,
                    City = tour.City,
                    Country = tour.Country,
                    
                });
            }
            PaginationVM<TourItemVM> paginationVM = new PaginationVM<TourItemVM>
            {
                TotalPage = Math.Ceiling((double)count / 5),
                CurrentPage = page,
                Items = dtos
            };
            return paginationVM;
        }
        public async Task<TourCreateVM> CreateGet(TourCreateVM vm)
        {
            vm.Cities = await _repository.GetAllCityAsync();
            vm.Countries = await _repository.GetAllCountriesAsync();
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
            if (!await _cityRepository.IsExistAsync(c => c.Id == dto.CityId))
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
                Url = await dto.HoverPhoto.CreateFileAsync(_env.WebRootPath, "rev-slider-files", "assets"),
            };
            Tour tour = new Tour
            {
                Name = dto.Name,
                DayCount = dto.DayCount,
                StartDate = dto.StartDate,
                StartTime = dto.StartTime,
                Description = dto.Description,
                IncludeDesc = dto.IncludeDesc,
                Includes = dto.Includes,
                Price = dto.Price,
                CountryId = dto.CountryId,
                CityId = dto.CityId,
                TourImages = new List<TourImage> { main, hover },
                Sale=dto.Sale
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
            await _repository.AddAsync(tour);
            await _repository.SaveChangesAsync();
            return true;
        }
        public async Task<TourGetVM> GetByIdAsync(int id)
        {
            Tour tour = await _repository.GetByIdAsync(id, false, nameof(Tour.Country),nameof(Tour.Category),nameof(Tour.City), nameof(Tour.TourImages));
            TourGetVM getVM = new TourGetVM
            {
                StartDate = tour.StartDate,
                Sale = tour.Sale,
                SalePrice = tour.SalePrice,
                StartTime = tour.StartTime,
                CityId = tour.CityId,
                CountryId = tour.CountryId,
                IncludeDesc = tour.IncludeDesc,
                Includes = tour.Includes,
                Name = tour.Name,
                Description = tour.Description,
                Price = tour.Price,
                TotalPrice = tour.TotalPrice,
                DayCount = tour.DayCount,
                EndTime = tour.EndTime,
                CategoryId= tour.CategoryId,
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
            if (tour.Category != null)
            {
                getVM.Category = new IncludeCategoryVM { Name = tour.Category.Name }; // Category null değilse CountryViewModel'i oluştur
            }
            getVM.TourImages = tour.TourImages;
            return getVM;
        }

        public async Task<TourUpdateVM> UpdateGet(int id, TourUpdateVM vm)
        {
            Tour tour = await _repository.GetFirstOrDefaultAsync(c => c.Id == id, false, nameof(Tour.TourImages),nameof(Tour.Category),nameof(Tour.Country), nameof(Tour.City));

            TourUpdateVM tourVM = new TourUpdateVM
            {
                Name = tour.Name,
                Price = tour.Price,
                DayCount = tour.DayCount,
                StartDate = tour.StartDate,
                StartTime = tour.StartTime,
                Description = tour.Description,
                IncludeDesc = tour.IncludeDesc,
                Includes = tour.Includes,
                CountryId = tour.CountryId,
                CityId = tour.CityId,
                CategoryId = tour.CategoryId,
                TourImages = tour.TourImages,
                Sale = tour.Sale,
                Cities = await _repository.GetAllCityAsync(),
                Countries = await _repository.GetAllCountriesAsync(),
                Categories=await _repository.
                
            };
            return tourVM;
        }
        public async Task<bool> Update(int id, TourUpdateVM dto, ModelStateDictionary modelstate)
        {
            Tour existed = await _repository.GetByIdAsync(id, false, nameof(Tour.Country), nameof(Tour.City), nameof(Tour.TourImages));
            if (existed == null)
            {
                return false;
            }
            if (!modelstate.IsValid)
            {
                return false;
            }
            if (existed.Name != dto.Name)
            {
                if (await _repository.IsExistAsync(p => p.Name == dto.Name))
                {
                    modelstate.AddModelError("Name", $"there is a product with the same {dto.Name}");
                }
            }
            if (!await _cityRepository.IsExistAsync(c => c.Id == dto.CityId))
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
            if (dto.MainPhoto is not null)
            {
                string filename = await dto.MainPhoto.CreateFileAsync(_env.WebRootPath, "rev-slider-files", "assets");
                TourImage existedImg = existed.TourImages.FirstOrDefault(pi => pi.IsPrimary == true);
                existedImg.Url.DeleteFile(_env.WebRootPath, "rev-slider-files", "assets");
                existed.TourImages.Remove(existedImg);
                existed.TourImages.Add(new TourImage
                {
                    IsPrimary = true,
                    Url = filename
                });
            }
            if (dto.HoverPhoto is not null)
            {
                string filename = await dto.HoverPhoto.CreateFileAsync(_env.WebRootPath, "rev-slider-files", "assets");
                TourImage existedImg = existed.TourImages.FirstOrDefault(pi => pi.IsPrimary == false);
                existedImg.Url.DeleteFile(_env.WebRootPath, "rev-slider-files", "assets");
                existed.TourImages.Remove(existedImg);
                existed.TourImages.Add(new TourImage
                {
                    IsPrimary = false,
                    Url = filename
                });
            }
            if (dto.ImageIds is null)
            {
                dto.ImageIds = new List<int>();
            }
            List<TourImage> removeable = existed.TourImages.Where(pi => !dto.ImageIds.Exists(imgId => imgId == pi.Id) && pi.IsPrimary == null).ToList();
            foreach (TourImage reimg in removeable)
            {
                reimg.Url.DeleteFile(_env.WebRootPath, "rev-slider-files", "assets");
                existed.TourImages.Remove(reimg);
            }
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
                existed.TourImages.Add(new TourImage
                {
                    IsPrimary = null,
                    Url = await photo.CreateFileAsync(_env.WebRootPath, "rev-slider-files", "assets")
                });
            }
            existed.Name = dto.Name;
            existed.Price = dto.Price;
            existed.Description = dto.Description;
            existed.CountryId = dto.CountryId;
            existed.CityId = dto.CityId;
            existed.DayCount = dto.DayCount;
            existed.StartDate = dto.StartDate;
            existed.StartTime = dto.StartTime;
            existed.IncludeDesc = dto.IncludeDesc;
            existed.Includes = dto.Includes;
            existed.TourImages = dto.TourImages;
            existed.Sale = dto.Sale;
            _repository.Update(existed);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            Tour existed = await _repository.GetByIdAsync(id, false, nameof(Tour.Country), nameof(Tour.City), nameof(Tour.TourImages));
            if (existed == null) throw new Exception("Product cant found");
            _repository.Delete(existed);
            await _repository.SaveChangesAsync();
        }

        public async Task<TourDetailVM> GetDetail(int id, TourDetailVM vM)
        {
            Tour tour = await _repository.GetByIdAsync(id, false, nameof(Tour.Country), nameof(Tour.City), nameof(Tour.TourImages));
            TourDetailVM getVM = new TourDetailVM
            {
                StartDate = tour.StartDate,
                StartTime = tour.StartTime,
                CityId = tour.CityId,
                CountryId = tour.CountryId,
                IncludeDesc = tour.IncludeDesc,
                Includes = tour.Includes,
                Name = tour.Name,
                Description = tour.Description,
                Price = tour.Price,
                DayCount = tour.DayCount,
                TourImages = tour.TourImages,
                Cities = await _repository.GetAllCityAsync(),
                Countries = await _repository.GetAllCountriesAsync()
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
