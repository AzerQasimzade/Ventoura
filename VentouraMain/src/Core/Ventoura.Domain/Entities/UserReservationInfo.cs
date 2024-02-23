using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventoura.Domain.Entities
{
    public class UserReservationInfo:BaseNameableEntity
    {
        public int TourId { get; set; } // Tur ile ilişkilendirilmiş rezervasyonun ID'si
        public Tour Tour { get; set; } // Tur bilgileri

        // Diğer rezervasyon bilgileri
        public int MemberCount { get; set; }
        public string Email { get; set; }
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }


    }
}
