using Microsoft.EntityFrameworkCore;
using Ventoura.Application.Abstractions.Repositories;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels;
using Ventoura.Application.ViewModels.Cities;
using Ventoura.Application.ViewModels.Countries;
using Ventoura.Application.ViewModels.Tours;
using Ventoura.Domain.Entities;

namespace Ventoura.Persistence.Implementations.Services
{
    public class TourService : ITourService
    {
        private readonly ITourRepository _repository;

        public TourService(ITourRepository repository)
        {
            _repository = repository;
        }
        public async Task<ICollection<TourItemVM>> GetAllAsync(int page, int take)
        {
            ICollection<Tour> tours =await  _repository.GetAll(skip: (page - 1) * take, take: take)
                .Include(c=>c.TourImages.Where(i=>i.IsPrimary==true))         
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
                   Url=tour.TourImages.FirstOrDefault(c=>c.IsPrimary==true).Url
                });
            }
            return dtos;
        }
        public async Task Create(TourCreateVM dto)
        {
            await _repository.AddAsync(new Tour
            {
                Name = dto.Name,
                DayCount = dto.DayCount,
                Sale = dto.Sale,
                SalePrice = dto.SalePrice,
                StartDate = dto.StartDate,
                StartTime = dto.StartTime,
                AdultCount = dto.AdultCount,
                ChildrenCount = dto.ChildrenCount,
                Description = dto.Description,
                EndTime = dto.EndTime,
                IncludeDesc = dto.IncludeDesc,
                Includes = dto.Includes,
                Price = dto.Price,
                TotalPrice = dto.TotalPrice
            });
            await _repository.SaveChangesAsync();
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
				EndTime = tour.EndTime,
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

		//public async Task<TourGetVM> GetByIdAsync(int id)
		//{
		//    Tour tour = await _repository.GetByIdAsync(id,false,nameof(Tour.Country),nameof(Tour.City));

		//    TourGetVM getVM = new TourGetVM
		//    {

		//        AdultCount = tour.AdultCount,
		//        StartDate = tour.StartDate,
		//        Sale = tour.Sale,
		//        SalePrice = tour.SalePrice,
		//        StartTime = tour.StartTime,
		//        ChildrenCount = tour.ChildrenCount,
		//        CityId = tour.CityId,
		//        CountryId = tour.CountryId,
		//        IncludeDesc = tour.IncludeDesc,
		//        Includes = tour.Includes,
		//        Name = tour.Name,
		//        Description = tour.Description,
		//        Price = tour.Price,
		//        TotalPrice = tour.TotalPrice,
		//        DayCount = tour.DayCount,
		//        EndTime = tour.EndTime,
		//    };
		//    getVM.City.Name = tour.City.Name;
		//    getVM.Country.Name = tour.Country.Name;
		//    getVM.TourImages = tour.TourImages;
		//    getVM.CityId = tour.CityId;
		//    getVM.CountryId = tour.CountryId;
		//    return getVM;
		//}
	}
}
