using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.Abstractions.Repositories;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Review;
using Ventoura.Domain.Entities;

namespace Ventoura.Persistence.Implementations.Services
{
    public class ReviewService : IReviewService
    {
        //private readonly IReviewRepository _repository;
        //private readonly IAuthService _service;
        //private readonly IHttpContextAccessor _http;

        //public ReviewService(IReviewRepository repository, IAuthService service, IHttpContextAccessor http)
        //{
        //    _repository = repository;
        //    _service = service;
        //    _http = http;
        //}
       

        public async Task<bool> CreateAsync(int id, ReviewCreateVm reviewCreateVm, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;
            //AppUser user = await _service.GetUserAsync(_http.HttpContext.User.Identity.Name);
            //if (user == null) throw new Exception("User Not found");
            //Review review = new Review
            //{
            //    Description = reviewCreateVm.Description,
            //    Quality = reviewCreateVm.Quality,
            //    RestaurantId = id,
            //    AppUserId = user.Id, b
            //    IsDeleted = false
            //};
            //await _repository.AddAsync(review);
 
            return true;
        }
        public async Task<ReviewCreateVm> CreatedAsync(ReviewCreateVm vm)
        {
            return new ReviewCreateVm();
        }



    }
}