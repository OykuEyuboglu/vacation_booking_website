using BitirmeProjesi1.DTOS;

namespace BitirmeProjesi1.DTOS
{
    public class HotelDetailDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string? MainFotoURL { get; set; }
        public decimal Rating { get; set; }
        public List<string> FotoURLs { get; set; }
        public List<string> FotoCaptions { get; set; }

    }
}