using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.ViewModels.Cities;

namespace Ventoura.Application.Abstractions.Services
{
    public interface ICityService
    {
        Task<ICollection<CityItemVM>> GetAllAsync(int page, int take);
        Task Create(CityCreateVM dto);

        //Task<GetTourDto> GetAsync(int id);
        //Task Update(int id, CityUpdateDto cityUpdateDto);
        //Task DeleteAsync(int id);
    }
}
