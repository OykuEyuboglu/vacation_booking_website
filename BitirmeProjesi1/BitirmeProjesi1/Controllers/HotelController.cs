using BitirmeProjesi1.DTOS;
using BitirmeProjesi1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BitirmeProjesi1.Controllers
{
    [Authorize]
    public class HotelController : Controller
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [AllowAnonymous] //herkes yönlenebilsin girişsiz
        public async Task<IActionResult> Index()
        {
            var hotels = await _hotelService.GetAllHotelsWithIdAsync();
            return View(hotels);
        }

        [AllowAnonymous] //herkes yönlenebilsin girişsiz
        public async Task<IActionResult> DetailHotel(string Id)
        {
            var hotel = await _hotelService.GetHotelDetailByIdAsync(Id);
            return View(hotel);

        }

        // Otel Rezervasyon Sayfası
        [HttpGet]
        public async Task<IActionResult> ReservationHotel(string hotelId)
        {
            var hotelDetails = await _hotelService.GetHotelByIdAsync(hotelId);
            return View(hotelDetails);
        }

        [HttpPost]
        public async Task<IActionResult> BookHotel([FromBody] HotelBookingDTO bookingDto)
        {
            if (ModelState.IsValid)
            {
                await _hotelService.BookHotelAsync(bookingDto);
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Aradığınız kriterlere uygun odamız mevcut değildir." });
        }

        [HttpPost]
        public IActionResult GetRoomId([FromBody] RoomRequestDTO request)
        {
            var roomId = _hotelService.GetRoomIdByTypeAndHotelId(request.RoomType, request.HotelID);
            
            if (roomId != null)
            {
                return Json(new { id = roomId });
            }
            return Json(new { id = (int?)null });
        }
    }
}
