using Microsoft.AspNetCore.Mvc;
using BitirmeProjesi1.Services;
using BitirmeProjesi1.DTOS;
using Microsoft.AspNetCore.Authorization;
using BitirmeProjesi1.Data.Entities;

namespace YourProjectName.Controllers
{
    public class AdminController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly IFlightService _flightService;
        public AdminController(IHotelService hotelService, IFlightService flightService)
        {
            _hotelService = hotelService;
            _flightService = flightService;
        }

        // Admin Dashboard
        [Authorize(Roles = "Admin")]
        public IActionResult Dashboard()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AddHotelAsync()
        {
            var hotels = await _hotelService.GetAllHotelsAsync();
            ViewBag.WhichPage = "AddHotelPage";

            return View(hotels);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddHotel(HotelCreateDTO hoteldto)
        {
            if (ModelState.IsValid)
            {
                TempData["DeleteStatus"] = "success"; // Başarılı
                await _hotelService.AddHotelAsync(hoteldto);
                return RedirectToAction("HotelList");
            }

            return View(hoteldto);
        }
        public async Task<IActionResult> HotelList()
        {

            var hotels = await _hotelService.GetAllHotelsAsync();
            return RedirectToAction("AddHotel");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> DeleteHotel()
        {
            if (TempData["MessageUpdate"] == null)
            {
                ViewBag.WhichPage = "DeleteHotelPage";
            }
            else
            {
                ViewBag.WhichPage = "UpdateHotelPage";
            }

            var hotels = await _hotelService.GetAllHotelsWithIdAsync();
            return View(hotels);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteHotel(string id)
        {
            await _hotelService.DeleteHotel(id);
            TempData["DeleteStatus"] = "success"; // Başarılı
            return RedirectToAction("DeleteHotel");
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> UpdateHotel(string Id)
        {
            ViewBag.WhichPage = "UpdateHotelPage";

            if (Id == null)
            {
                TempData["MessageUpdate"] = "It comes from UpdateHotel";
                return RedirectToAction("DeleteHotel");
            }
            else
            {
                var hotel = await _hotelService.GetHotelByIdAsync(Id);
                return View(hotel);

            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateHotel(HotelDTO hotelDTO)
        {
            if (ModelState.IsValid)
            {
                TempData["MessageUpdate"] = "It comes from UpdateHotel";
                await _hotelService.UpdateHotelAsync(hotelDTO);

                TempData["DeleteStatus"] = "success"; // Başarılı
                return RedirectToAction("DeleteHotel");
            }

            return View(hotelDTO);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddFoto()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddFotoPost(HotelImageCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                await _hotelService.AddFotoAsync(model);
                return RedirectToAction("AddFoto");
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> DeleteFoto(string id)
        {
            ViewBag.WhichPage = "FotoDeletePage";

            if (id != null)
            {
                var finding = await _hotelService.FindPhotosAsync(id);
                return View(finding);
            }

            var hotels = await _hotelService.GetAllHotelsWithIdAsync();

            ViewData["Hotels"] = hotels;
            return View();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteFotos(string id)
        {
            await _hotelService.DeleteFoto(id);

            ViewBag.WhichPage = "FotoDeletePage";
            TempData["DeleteStatus"] = "success"; // Başarılı

            return RedirectToAction("DeleteFoto");
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AddRoomAsync()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddRoom(RoomRequestDTO roomRequestDTO)
        {
            if (ModelState.IsValid)
            {
                await _hotelService.AddRoomAsync(roomRequestDTO);
                return RedirectToAction("RoomList");
            }

            return View();
        }

        public async Task<IActionResult> RoomList()
        {

            var rooms = await _hotelService.GetAllHotelsAsync();
            return RedirectToAction("AddRoom");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> DeleteRoom(string id)
        {
            if (TempData["MessageUpdate"] == null)
            {
                ViewBag.WhichPage = "RoomDeletePage";
            }
            else
            {
                ViewBag.WhichPage = "RoomUpdatePage";
            }

            if (id != null)
            {
                var finding = await _hotelService.FindRoomsAsync(id);
                return View(finding);
            }

            var hotels = await _hotelService.GetAllHotelsWithIdAsync();

            ViewData["Hotels"] = hotels;
            return View();

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteRoomm(string id)
        {
            await _hotelService.DeleteRoom(id);
            TempData["DeleteStatus"] = "success"; // Başarılı
            return RedirectToAction("DeleteRoom");
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> UpdateRoom(string Id)
        {
            ViewBag.WhichPage = "RoomUpdatePage";

            if (Id == null)
            {
                TempData["MessageUpdate"] = "It comes from UpdateRoom";

                return RedirectToAction("DeleteRoom");
            }
            else
            {
                var finding = await _hotelService.FindRoomsAsync(Id);

                if (finding.Count == 0)
                {
                   var room = await _hotelService.GetRoomByIdAsync(Id); //otel değil oda arıyor buraya girdiyse

                    if(room != null)room.Id = Id;
                    var roomList = new List<RoomDTO> { room }; // Tek oda verisini listeye çeviriyoruz
                    return View(roomList); // Listeyi View'a gönderiyoruz
                }
                return View(finding);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateRoom(RoomDTO roomDTO)
        {
            if (ModelState.IsValid)
            {
                ViewBag.WhichPage = "RoomUpdatePage";
                TempData["MessageUpdate"] = "It comes from UpdateRoom";
                await _hotelService.UpdateRoomAsync(roomDTO);
                TempData["DeleteStatus"] = "success"; // Başarılı
                return RedirectToAction("DeleteRoom");
            }

            return View(roomDTO);
        }
    }
}
