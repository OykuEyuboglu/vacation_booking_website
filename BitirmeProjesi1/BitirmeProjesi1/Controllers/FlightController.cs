using BitirmeProjesi1.Data.Context;
using BitirmeProjesi1.Data.Entities;
using BitirmeProjesi1.DTOS;
using BitirmeProjesi1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BitirmeProjesi1.Controllers
{
    [Authorize]
    public class FlightController : Controller
    {
        private readonly IFlightService _flightService;
        private readonly ProjectContext _db;

        public FlightController(IFlightService flightService, ProjectContext db)
        {
            _flightService = flightService;
            _db = db;
        }


        [AllowAnonymous] //herkes yönlenebilsin girişsiz
        public async Task<IActionResult> Index()
        {
            var flights = await _flightService.GetAllFlightsWithIdAsync();

            var message = TempData["Message"];
            var popupControl = TempData["Control"];

            if (message != null)
            {
                ViewBag.WhichPage = "UpdateFlightPage";
            }
            else
            {
                ViewBag.WhichPage = "IndexFlightPage";

            }

            if (popupControl != null)
            {
                TempData["DeleteStatus"] = "success"; // Başarılı
                ViewBag.WhichPage = "UpdateFlightPage";
            }

            var flightList = flights.Select(flight => new FlightDTO
            {
                ID = flight.ID,
                DepartureLocation = flight.DepartureLocation,
                ArrivalLocation = flight.ArrivalLocation,
                DepartureDate = flight.DepartureDate,
                ArrivalDate = flight.ArrivalDate,
                Price = flight.Price,
                SeatsAvailable = flight.SeatsAvailable,
            }).ToList();

            return View(flightList);
        }

        [HttpGet]
        public async Task<IActionResult> AddFlight()
        {

            var flights = await _flightService.GetAllFlightsWithIdAsync();
            ViewBag.WhichPage = "AddFlightPage";

            var flightList = flights.Select(flight => new FlightDTO
            {
                ID = flight.ID,
                DepartureLocation = flight.DepartureLocation,
                ArrivalLocation = flight.ArrivalLocation,
                DepartureDate = flight.DepartureDate,
                ArrivalDate = flight.ArrivalDate,
                Price = flight.Price,
                SeatsAvailable = flight.SeatsAvailable,
            }).ToList();

            return View(flightList);
        }

        // Admin uçuş ekleme işlemi
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddFlight(FlightCreateDTO flightDto)
        {
            if (ModelState.IsValid)
            {
                await _flightService.AddFlightAsync(flightDto);
                TempData["DeleteStatus"] = "success"; // Başarılı

                return RedirectToAction("AddFlight");
            }

            var flights = await _flightService.GetAllFlightsAsync();
            return View(flights);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> DeleteFlight()
        {
            var flights = await _flightService.GetAllFlightsWithIdAsync();

            ViewBag.WhichPage = "DeleteFlightPage";

            var flightList = flights.Select(flight => new FlightDTO
            {
                ID = flight.ID,
                DepartureLocation = flight.DepartureLocation,
                ArrivalLocation = flight.ArrivalLocation,
                DepartureDate = flight.DepartureDate,
                ArrivalDate = flight.ArrivalDate,
                Price = flight.Price,
                SeatsAvailable = flight.SeatsAvailable,
            }).ToList();

            return View(flightList);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteFlightConfirmed(string id)
        {
            await _flightService.DeleteFlightAsync(id);

            TempData["DeleteStatus"] = "success"; // Başarılı
            return RedirectToAction("DeleteFlight");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> UpdateFlight(string Id)
        {
            if (Id == null)
            {
                TempData["Message"] = "It is the first step";
                return RedirectToAction("Index");
            }
            else
            {
                var flight = await _flightService.GetFlightByIdAsync(Id);
                return View(flight);

            }
        }

        // Admin uçuş güncelleme işlemi
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateFlight(FlightUpdateDTO flightDto)
        {
            if (ModelState.IsValid)
            {
                await _flightService.UpdateFlightAsync(flightDto);

                TempData["Control"] = "Done. Now you show the popup";
                return RedirectToAction("Index");
            }

            return View(flightDto);
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> PreReservation(string id)
        {
            var flight = await _flightService.GetFlightByIdAsync(id);
            return View(flight);
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> BookFlight([FromBody] FlightBookingDTO bookingDto)
        {
            var flight = await _flightService.GetFlightByIdAsync(bookingDto.FlightID);
            bookingDto.CheckInDate = flight.ArrivalDate;
            bookingDto.CheckOutDate = flight.DepartureDate;

            if (ModelState.IsValid)
            {
                await _flightService.BookFlightAsync(bookingDto);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

    }
}