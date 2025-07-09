using BitirmeProjesi1.Data.Entities;
using BitirmeProjesi1.DTOS;

namespace BitirmeProjesi1.Services
{
    public interface IHotelService
    {
        Task<IEnumerable<HotelCreateDTO>> GetAllHotelsAsync();
        Task<IEnumerable<HotelDTO>> GetAllHotelsWithIdAsync();
        Task<HotelDTO> GetHotelByIdAsync(string id);
        IEnumerable<Room> GetRoomsByHotelId(string hotelId);
        Task<IEnumerable<RoomRequestDTO>> GetAllRoomsWithIdAsync();


        Task BookHotelAsync(HotelBookingDTO bookingDto);
        Task<HotelDetailDTO> GetHotelDetailByIdAsync(string id);
        string? GetRoomIdByTypeAndHotelId(string roomType, string hotelId);


        Task<List<HotelImageDTO>> FindPhotosAsync(string id);
        Task<List<string>> GetHotelFotoForSlider();
        Task<List<string>> GetHotelDescriptionForSlider();
        Task<List<string>> GetHotelNameForSlider();

        Task DeleteFoto(string id);
        Task AddFotoAsync(HotelImageCreateDTO hotelImageCreateDTO);

        Task AddHotelAsync(HotelCreateDTO hotelDto);
        Task UpdateHotelAsync(HotelDTO hotelDto);
        Task DeleteHotel(string id);
        Task AddRoomAsync(RoomRequestDTO roomRequestDTO);
        Task UpdateRoomAsync(RoomDTO roomDTO);
        Task DeleteRoom(string id);
        Task<RoomDTO> GetRoomByIdAsync(string id);

        Task<List<RoomDTO>> FindRoomsAsync(string id);
        Task<IEnumerable<ReservationListDTO>> GetUserReservationsAsync(string userId);
    }
}
