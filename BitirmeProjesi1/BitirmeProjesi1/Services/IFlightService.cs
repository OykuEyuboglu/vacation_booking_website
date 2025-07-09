using BitirmeProjesi1.DTOS;

namespace BitirmeProjesi1.Services
{
    public interface IFlightService
    {
        Task<List<FlightCreateDTO>> GetAllFlightsAsync();
        Task<List<FlightDTO>> GetAllFlightsWithIdAsync();
        Task<List<FlightDTO>> FindFlightsAsync(string departureLocation, string arrivalLocation, int adultsNumber, int childrenNumber);
        Task<FlightDTO> GetFlightByIdAsync(string id);
        Task AddFlightAsync(FlightCreateDTO flightDto);
        Task UpdateFlightAsync(FlightUpdateDTO flightDto);
        Task DeleteFlightAsync(string id);
        Task BookFlightAsync(FlightBookingDTO bookingDto);
    }
}
