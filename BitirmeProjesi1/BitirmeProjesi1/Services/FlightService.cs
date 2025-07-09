using BitirmeProjesi1.Data.Context;
using BitirmeProjesi1.Data.Entities;
using BitirmeProjesi1.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BitirmeProjesi1.Services
{
    public class FlightService : IFlightService
    {
        private readonly ProjectContext _db;

        public FlightService(ProjectContext db)
        {
            _db = db;
        }

        // Tüm uçuşları listele
        public async Task<List<FlightCreateDTO>> GetAllFlightsAsync()
        {
            var flights = await _db.Flights.ToListAsync();
            return flights.Select(f => new FlightCreateDTO
            {
                Airline = f.Airline,
                DepartureLocation = f.DepartureLocation,
                ArrivalLocation = f.ArrivalLocation,
                DepartureDate = f.DepartureDate,
                ArrivalDate = f.ArrivalDate,
                Price = f.Price,
                SeatsAvailable = f.SeatsAvailable
            }).ToList();
        }

        //Delete için
        public async Task<List<FlightDTO>> GetAllFlightsWithIdAsync()
        {
            var flights = await _db.Flights.ToListAsync();
            return flights.Select(f => new FlightDTO
            {
                ID = f.ID,
                Airline = f.Airline,
                DepartureLocation = f.DepartureLocation,
                ArrivalLocation = f.ArrivalLocation,
                DepartureDate = f.DepartureDate,
                ArrivalDate = f.ArrivalDate,
                SeatsAvailable = f.SeatsAvailable,
                Price = f.Price,
            }).ToList();
        }

        // Yeni uçuş ekle
        public async Task AddFlightAsync(FlightCreateDTO flightDto)
        {
            var flight = new Flight
            {
                Airline = flightDto.Airline,
                DepartureLocation = flightDto.DepartureLocation,
                ArrivalLocation = flightDto.ArrivalLocation,
                DepartureDate = flightDto.DepartureDate,
                ArrivalDate = flightDto.ArrivalDate,
                Price = flightDto.Price,
                SeatsAvailable = flightDto.SeatsAvailable
            };

            await _db.Flights.AddAsync(flight);
            await _db.SaveChangesAsync();
        }

        // ID'ye göre uçuşu getir
        public async Task<FlightDTO> GetFlightByIdAsync(string id)
        {
            var flight = await _db.Flights.FindAsync(id);
            if (flight == null) return null;

            return new FlightDTO
            {
                ID = flight.ID,
                Airline = flight.Airline,
                DepartureLocation = flight.DepartureLocation,
                ArrivalLocation = flight.ArrivalLocation,
                DepartureDate = flight.DepartureDate,
                ArrivalDate = flight.ArrivalDate,
                Price = flight.Price,
                SeatsAvailable = flight.SeatsAvailable
            };
        }

        // Uçuşu güncelle
        public async Task UpdateFlightAsync(FlightUpdateDTO flightDto)
        {
            var flight = await _db.Flights.FindAsync(flightDto.ID);
            if (flight == null) return;

            flight.Airline = flightDto.Airline;
            flight.DepartureLocation = flightDto.DepartureLocation;
            flight.ArrivalLocation = flightDto.ArrivalLocation;
            flight.DepartureDate = flightDto.DepartureDate;
            flight.ArrivalDate = flightDto.ArrivalDate;
            flight.Price = flightDto.Price;
            flight.SeatsAvailable = flightDto.SeatsAvailable;

            _db.Flights.Update(flight);
            await _db.SaveChangesAsync();
        }

        // Uçuşu sil
        public async Task DeleteFlightAsync(string id)
        {
            var flight = await _db.Flights.FindAsync(id);
            if (flight == null) return;

            _db.Flights.Remove(flight);
            await _db.SaveChangesAsync();
        }

        // Seçili uçuşları listele
        public async Task<List<FlightDTO>> FindFlightsAsync(string departureLocation, string arrivalLocation, int adultsNumber, int childrenNumber)
        {
            // Yetişkin ve çocuk sayısı toplamı
            int totalPassengers = adultsNumber + childrenNumber;

            // Uçuşları filtrele
            var flights = await _db.Flights.Where(f => f.DepartureLocation == departureLocation && f.ArrivalLocation == arrivalLocation)
                .Where(f => f.SeatsAvailable >= totalPassengers)
                .ToListAsync();

            return flights.Select(f => new FlightDTO
            {
                Airline = f.Airline,
                DepartureLocation = f.DepartureLocation,
                ArrivalLocation = f.ArrivalLocation,
                DepartureDate = f.DepartureDate,
                ArrivalDate = f.ArrivalDate,
                Price = f.Price,
                SeatsAvailable = f.SeatsAvailable
            }).ToList();
        }

        public async Task BookFlightAsync(FlightBookingDTO bookingDto)
        {
                // Uçuşu bul
                var flight = await _db.Flights.FirstOrDefaultAsync(f => f.ID == bookingDto.FlightID);
                var user = await _db.Users.FirstOrDefaultAsync(f => f.ID == bookingDto.UserID);
                // Uygun koltuk sayısını kontrol et
                if (flight.SeatsAvailable < bookingDto.PassengerCount)
                {
                    throw new Exception("Yeterli boş koltuk yok.");
                }

                // FlightBooking oluştur
                var booking = new FlightBooking
                {
                    Flight = flight,
                    UserID = bookingDto.UserID,
                    CheckInDate = bookingDto.CheckInDate,
                    CheckOutDate = bookingDto.CheckOutDate,
                };

                // FlightPayment oluştur
                var payment = new FlightPayment
                {
                    FlightBooking = booking,
                    Method = bookingDto.PaymentMethod,
                    PaymentDate = bookingDto.CheckInDate
                };

                booking.FlightPayment = payment;
                payment.FlightBookingID = booking.ID;
                booking.User = user;

                flight.SeatsAvailable -= bookingDto.PassengerCount;

                _db.FlightPayments.Add(payment);
                _db.FlightBookings.Add(booking);
                _db.Flights.Update(flight);

                await _db.SaveChangesAsync();

        }
    }




}