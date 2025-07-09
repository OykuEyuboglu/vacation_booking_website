namespace BitirmeProjesi1.Data.Entities
{
    public class HotelImage
    {
        public string ID { get; set; }
        public string ImageURL { get; set; }
        public string? Caption { get; set; } //image description
        public DateTime UploadedAt { get; set; } = DateTime.Now;

        public string HotelID { get; set; }
        public Hotel Hotel { get; set; }

        public HotelImage()
        {
            ID = Guid.NewGuid().ToString();
        }
    }
}
