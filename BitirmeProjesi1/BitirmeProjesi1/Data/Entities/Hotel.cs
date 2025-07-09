namespace BitirmeProjesi1.Data.Entities
{
    public class Hotel : Entity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public decimal Rating { get; set; }
        public string Description { get; set; }
        public string? MainFotoURL { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public ICollection<HotelImage> HotelImages { get; set; }
    }
}
