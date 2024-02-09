using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.ViewModels;
using Ventoura.Application.ViewModels.Tours;

namespace Ventoura.Application.Abstractions.Services
{
    public interface ITourService
    {
        Task<ICollection<TourItemVM>> GetAllAsync(int page, int take);
        Task Create(TourCreateVM vm);
        Task<TourGetVM> GetByIdAsync(int id);
        //Task Update(int id, TourUpdateVM tourUpdateDto);
        //Task DeleteAsync(int id);
    }
}
