using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventoura.Application.ViewModels.Review
{
    public class ReviewCreateVm
    {
        public int Quality { get; set; }
        public string Description { get; set; } = null!;
        public int TourId { get; set; }
    }
}
