using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.ViewModels.Review;

namespace Ventoura.Application.Abstractions.Services
{
    public interface IReviewService
    {
        Task<bool> CreateAsync(int id, ReviewCreateVm vm, ModelStateDictionary modelState);
        Task<ReviewCreateVm> CreatedAsync(ReviewCreateVm vm);

    }
}
