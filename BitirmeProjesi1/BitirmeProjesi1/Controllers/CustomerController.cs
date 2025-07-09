using Microsoft.AspNetCore.Mvc;
using BitirmeProjesi1.DTOS;
using BitirmeProjesi1.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BitirmeProjesi1.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace YourProjectName.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly IUserService _userService;

        public CustomerController(IHotelService hotelService, IUserService userService)
        {
            _hotelService = hotelService;
            _userService = userService;
        }

        public async Task<IActionResult> Dashboard(string customerId)
        {
            var customerProfile = await _userService.GetCustomerProfileAsync(customerId);
            return View(customerProfile);
        }

        [HttpGet] //rezervasyon için userID alınıyor jquery ile view'dan
        public IActionResult GetUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Kullanıcının ID'sini almak için
            return Json(new { id = userId }); // ID ve kullanıcı adını döndür

        }

        [HttpGet]
        public async Task<IActionResult> GetReservations(string userId)
        {
            var reservations = await _hotelService.GetUserReservationsAsync(userId);
            return View(reservations);
        }

    }
}
