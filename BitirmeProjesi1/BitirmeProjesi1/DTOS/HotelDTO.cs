namespace BitirmeProjesi1.DTOS
{
    public class HotelDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public decimal Rating { get; set; }
        public string? MainFotoURL { get; set; }
        public string Description { get; set; }
        public decimal STARTPricePerNight { get; set; }
    }
}
