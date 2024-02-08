using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.ViewModels.Tours;
using Ventoura.Domain.Entities;

namespace Ventoura.Application.MappingProfiles
{
    public class TourProfile:Profile
    {
        public TourProfile()
        {
            CreateMap<TourGetVM,Tour>().ReverseMap();
            CreateMap<TourItemVM, Tour>().ReverseMap();
        }
    }
}
