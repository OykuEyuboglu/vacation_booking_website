namespace BitirmeProjesi1.Data.Entities
{
    public class Flight : Entity
    {
        public string Airline { get; set; }
        public string DepartureLocation { get; set; }
        public string ArrivalLocation { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public decimal Price { get; set; }
        public int SeatsAvailable { get; set; }
        public ICollection<FlightBooking> FlightBookings { get; set; }
    }
}
