namespace BitirmeProjesi1.Data.Entities
{
    public class Room : Entity
    {
        public string HotelID { get; set; }
        public string RoomType { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
        public Hotel Hotel { get; set; }
        public ICollection<HotelBooking> HotelBookings { get; set; }
    }
}
