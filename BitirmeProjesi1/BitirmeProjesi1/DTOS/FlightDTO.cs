namespace BitirmeProjesi1.DTOS
{
    public class FlightDTO
    {
        public string ID { get; set; }
        public string Airline { get; set; }
        public string DepartureLocation { get; set; }
        public string ArrivalLocation { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public decimal Price { get; set; }
        public int SeatsAvailable { get; set; }
    }
}
