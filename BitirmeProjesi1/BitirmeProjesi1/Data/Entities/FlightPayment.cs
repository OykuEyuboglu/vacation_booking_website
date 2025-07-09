namespace BitirmeProjesi1.Data.Entities
{
    public class FlightPayment : PaymentBase
    {
        public string FlightBookingID { get; set; }
        public FlightBooking FlightBooking { get; set; }
    }
}
