using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventoura.Application.ViewModels.Review
{
    public class ReviewItemVm
    {
        public int Id { get; set; }
        public int Quality { get; set; }
        public string Description { get; set; } = null!;

    }
}
