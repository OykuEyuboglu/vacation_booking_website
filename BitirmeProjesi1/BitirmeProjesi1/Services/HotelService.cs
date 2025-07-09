using BitirmeProjesi1.Data.Context;
using BitirmeProjesi1.Data.Entities;
using BitirmeProjesi1.DTOS;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace BitirmeProjesi1.Services
{
    public class HotelService : IHotelService
    {
        private readonly ProjectContext _db;

        public HotelService(ProjectContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<HotelCreateDTO>> GetAllHotelsAsync()
        {
            return await _db.Hotels
                    .Select(h => new HotelCreateDTO
                    {
                        Name = h.Name,
                        Location = h.Location,
                        Description = h.Description,
                        Rating = h.Rating,
                        MainFotoURL = h.MainFotoURL,
                    })
                    .ToListAsync();
        }

        public async Task<IEnumerable<HotelDTO>> GetAllHotelsWithIdAsync()
        {
            return await _db.Hotels
                    .Select(h => new HotelDTO
                    {
                        Id = h.ID,
                        Name = h.Name,
                        Location = h.Location,
                        Description = h.Description,
                        MainFotoURL = h.MainFotoURL,
                        Rating = h.Rating,
                        STARTPricePerNight = h.Rooms.OrderBy(r => r.PricePerNight).Select(r => r.PricePerNight).FirstOrDefault() // En ucuz odanın fiyatı
                    })
                    .ToListAsync();
        }

        public async Task<IEnumerable<RoomRequestDTO>> GetAllRoomsWithIdAsync()
        {
            return await _db.Rooms
                    .Select(h => new RoomRequestDTO
                    {
                        HotelID = h.ID,
                        IsAvailable = h.IsAvailable,
                        RoomType = h.RoomType,
                    })
                    .ToListAsync();
        }

        public async Task<List<string>> GetHotelFotoForSlider()
        {
            var hotelFotos = await _db.Hotels
                .Select(h => h.MainFotoURL)
                .ToListAsync();

            return hotelFotos;
        }
        public async Task<List<string>> GetHotelDescriptionForSlider()
        {
            var hotelDescription = await _db.Hotels
                .Select(h => h.Description)
                .ToListAsync();

            return hotelDescription;
        }
        public async Task<List<string>> GetHotelNameForSlider()
        {
            var hotelName = await _db.Hotels
                .Select(h => h.Name)
                .ToListAsync();

            return hotelName;
        }

        public async Task<HotelDTO> GetHotelByIdAsync(string id)
        {
            var hotel = await _db.Hotels
                .Include(h => h.Rooms)
                .FirstOrDefaultAsync(h => h.ID == id);

            if (hotel == null) return null;

            // En ucuz odanın fiyatını bul
            var startPricePerNight = hotel.Rooms
                .OrderBy(r => r.PricePerNight)
                .Select(r => r.PricePerNight)
                .FirstOrDefault(); // İlk fiyatı al (en ucuz)

            return new HotelDTO
            {
                Id = hotel.ID,
                Name = hotel.Name,
                Location = hotel.Location,
                Description = hotel.Description,
                MainFotoURL = hotel.MainFotoURL,
                Rating = hotel.Rating,
                STARTPricePerNight = startPricePerNight
            };
        }

        public async Task<List<HotelImageDTO>> FindPhotosAsync(string id)
        {
            var photos = await _db.HotelImages
                .Where(photo => photo.HotelID == id)
                .ToListAsync();

            return photos.Select(photo => new HotelImageDTO
            {
                ID = photo.ID,
                ImageURL = photo.ImageURL,
                Caption = photo.Caption,
                hotelId = photo.HotelID
            }).ToList();
        }

        public async Task AddFotoAsync(HotelImageCreateDTO hotelImageCreateDto)
        {
            var hotelImage = new HotelImage
            {
                Caption = hotelImageCreateDto.Caption,
                ImageURL = hotelImageCreateDto.ImageURL,
                HotelID = hotelImageCreateDto.hotelId,
            };

            _db.HotelImages.Add(hotelImage);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteFoto(string id)
        {
            var foto = await _db.HotelImages.FindAsync(id);
            if (foto == null) return;

            _db.HotelImages.Remove(foto);
            await _db.SaveChangesAsync();
        }

        public async Task AddHotelAsync(HotelCreateDTO hotelDto)
        {
            var hotel = new Hotel
            {
                Name = hotelDto.Name,
                Location = hotelDto.Location,
                Description = hotelDto.Description,
                MainFotoURL = hotelDto.MainFotoURL,
                Rating = hotelDto.Rating,
            };

            _db.Hotels.Add(hotel);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateHotelAsync(HotelDTO hotelDto)
        {
            var hotel = await _db.Hotels.FindAsync(hotelDto.Id);
            if (hotel == null) return;

            hotel.Name = hotelDto.Name;
            hotel.Location = hotelDto.Location;
            hotel.Description = hotelDto.Description;
            hotel.MainFotoURL = hotelDto.MainFotoURL;
            hotel.Rating = hotelDto.Rating;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteHotel(string id)
        {
            var hotel = await _db.Hotels.FindAsync(id);
            if (hotel == null) return;

            _db.Hotels.Remove(hotel);
            await _db.SaveChangesAsync();
        }

        public async Task AddRoomAsync(RoomRequestDTO roomRequestDTO)
        {
            var room = new Room
            {
                HotelID = roomRequestDTO.HotelID,
                IsAvailable = false,
                RoomType = roomRequestDTO.RoomType,
                PricePerNight = roomRequestDTO.PricePerNight
            };

            _db.Rooms.Add(room);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateRoomAsync(RoomDTO roomDTO)
        {
            var room = await _db.Rooms.FindAsync(roomDTO.Id);
            if (room == null) return;

            room.UpdatedAt = DateTime.Now;
            room.PricePerNight = roomDTO.PricePerNight;
            room.RoomType = roomDTO.RoomType;
            room.IsAvailable = roomDTO.IsAvailable;
            room.HotelID = roomDTO.HotelID;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteRoom(string id)
        {
            var room = await _db.Rooms.FindAsync(id);
            if (room == null) return;

            _db.Rooms.Remove(room);
            await _db.SaveChangesAsync();
        }

        public async Task<List<RoomDTO>> FindRoomsAsync(string id)
        {

            var rooms = await _db.Rooms
                .Where(room => room.HotelID == id)
                .ToListAsync();

            return rooms.Select(room => new RoomDTO
            {
                Id = room.ID,
                HotelID = room.HotelID,
                IsAvailable = room.IsAvailable,
                PricePerNight = room.PricePerNight,
                RoomType = room.RoomType
            }).ToList();
        }

        public async Task<RoomDTO> GetRoomByIdAsync(string id)
        {
            var room = await _db.Rooms.FindAsync(id);
            if (room == null) return null;

            return new RoomDTO
            {
                PricePerNight = room.PricePerNight,
                RoomType = room.RoomType,
                IsAvailable = room.IsAvailable,
                HotelID = room.HotelID
            };
        }

        public async Task<HotelDetailDTO> GetHotelDetailByIdAsync(string id)
        {
            var hotel = await _db.Hotels
                .Include(h => h.HotelImages)
                .FirstOrDefaultAsync(h => h.ID == id);

            if (hotel == null) return null;

            return new HotelDetailDTO
            {
                Id = hotel.ID,
                Name = hotel.Name,
                Location = hotel.Location,
                Description = hotel.Description,
                MainFotoURL = hotel.MainFotoURL,
                Rating = hotel.Rating,

                FotoURLs = hotel.HotelImages
                        .Where(img => img.HotelID == id)
                        .Select(img => img.ImageURL)
                        .ToList(),

                FotoCaptions = hotel.HotelImages
                        .Where(img => img.HotelID == id)
                        .Select(img => img.Caption)
                        .ToList()


            };
        }

        public async Task BookHotelAsync(HotelBookingDTO bookingDto)
        {
            var room = await _db.Rooms.FirstOrDefaultAsync(r => r.ID == bookingDto.RoomID);

            if (room == null)
            {
                throw new Exception("Room is not available");
            }

            // HotelBooking oluştur
            var booking = new HotelBooking
            {
                RoomID = bookingDto.RoomID,
                CheckInDate = bookingDto.CheckInDate,
                CheckOutDate = bookingDto.CheckOutDate,
                UserID = bookingDto.UserID,
            };

            // HotelPayment oluştur
            var payment = new HotelPayment
            {
                HotelBookingID = booking.ID,
                Method = bookingDto.PaymentMethod,
                PaymentDate = booking.CheckInDate
            };

            booking.HotelPayment = payment;
            _db.HotelBookings.Add(booking);

            room.IsAvailable = true; // Odası rezerve edildiği için "Dolu" olarak ayarlıyoruz

            _db.Rooms.Update(room);
            await _db.SaveChangesAsync();
        }

        public string? GetRoomIdByTypeAndHotelId(string roomType, string hotelId)
        {
            var room = GetRoomsByHotelId(hotelId)
                                       .FirstOrDefault(r => r.RoomType == roomType);
            return room?.ID;
        }

        public IEnumerable<Room> GetRoomsByHotelId(string hotelId)
        {
            return _db.Rooms.Where(r => r.HotelID == hotelId).ToList();
        }

        public async Task<IEnumerable<ReservationListDTO>> GetUserReservationsAsync(string userId)
        {
            return await _db.HotelBookings
                .Where(r => r.UserID == userId)
                .Include(r => r.Room)
                .Include(r => r.HotelPayment)
                .Include(r => r.User)
                .Select(r => new ReservationListDTO
                {
                    PaymentMethod = r.HotelPayment.Method,
                    RoomType = r.Room.RoomType,
                    CheckInDate = r.CheckInDate,
                    CheckOutDate = r.CheckOutDate,
                    Username = r.User.FirstName + " " + r.User.LastName,
                    HotelName = r.Room.Hotel.Name

                })
                .ToListAsync();
        }
    }
}

