using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Application.ViewModels.Tours;
using Ventoura.Application.ViewModels.Users;
using Ventoura.Domain.Entities;
using Ventoura.Domain.Exceptions;

namespace Ventoura.UI.Areas.VentouraAdmin.Controllers
{
    [Area("VentouraAdmin")]
    public class ReservationController : Controller
    {
        private readonly IReserveService _service;
        private readonly IMailService _mailService;
        private readonly UserManager<AppUser> _usermanager;

        public ReservationController(IReserveService service,IMailService mailService,UserManager<AppUser> usermanager)
        {
            _service = service;
            _mailService = mailService;
            _usermanager = usermanager;
        }
        public async Task<IActionResult> TourReserveList()
        {
            return View(await _service.GetAllAsyncForReserve());
        }
        
        //<------------------------------------>
        public async Task<IActionResult> ConfirmReservation(int reservationId)
        {
            // Burada, reservationId parametresini kullanarak rezervasyonu veritabanından alabilirsiniz.
            var reservation = await _service.GetReservationByIdAsync(reservationId);
            if (reservation == null)
            {
                throw new NotFoundException("404.Reservation cannot Found"); 
            }
            
            var userEmail = reservation.Email;
            // Onay linki oluşturun
            var confirmationLink = Url.Action("CheckOut", "Basket", new { reservationId = reservation.Id, area = "Ventoura.UI.Controllers" }, HttpContext.Request.Scheme);

            // E-posta gönderme işlemi
            var mailRequest = new MailRequestVM
            {
                ToEmail = userEmail,
                Subject = "Reservation Confirmation",
                Body = $"Your reservation has been confirmed. You can view your reservation details <a href='{confirmationLink}'>here</a>."
            };
            await _mailService.SendEmailAsync(mailRequest);
            // Rezervasyonu onayladıktan sonra veritabanında güncelleme yapmayı unutmayın
            // Örneğin:
            //reservation.Status = "accepted";
            //await _service.UpdateReservationStatusAsync(reservationId, "accepted");
            return RedirectToAction(nameof(TourReserveList)); // Rezervasyon listesine geri dönün.
        }
        public async Task<IActionResult> RejectReservation(int reservationId)
        {
            // Rezervasyonu veritabanından al
            var reservation = await _service.GetReservationByIdAsync(reservationId);

            if (reservation == null)
            {
                throw new NotFoundException("404.Reservation not found");
            }

            var userEmail = reservation.Email;

            // E-posta gönderme işlemi
            var mailRequest = new MailRequestVM
            {
                ToEmail = userEmail,
                Subject = "Reservation Rejected",
                Body = "Your reservation has been rejected."
            };
            await _mailService.SendEmailAsync(mailRequest);

            // Rezervasyonun durumunu "reddedildi" olarak güncelle
            //reservation.Status ="rejected";
            //await _service.UpdateReservationStatusAsync(reservationId,"rejected");
            return RedirectToAction(nameof(TourReserveList)); // Rezervasyon listesine geri dönün.
        }
    }
}
