namespace BitirmeProjesi1.DTOS
{
    public class RoomRequestDTO
    {
        public string RoomType { get; set; }
        public string HotelID { get; set; }
        public bool IsAvailable { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
