namespace BitirmeProjesi1.Data.Entities
{
    public class User : Entity
    {
        public string UserType { get; set; } // customer or admin
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<HotelBooking> HotelBookings { get; set; }
        public ICollection<FlightBooking> FlightBookings { get; set; }
    }
}
