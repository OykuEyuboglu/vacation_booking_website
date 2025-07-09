using BitirmeProjesi1.Data.Entities;

namespace BitirmeProjesi1.DTOS
{
    public class HotelImageDTO
    {
        public string ID { get; set; }
        public string ImageURL { get; set; }
        public string? Caption { get; set; } //image description
        public string hotelId { get; set; }
    }
}
