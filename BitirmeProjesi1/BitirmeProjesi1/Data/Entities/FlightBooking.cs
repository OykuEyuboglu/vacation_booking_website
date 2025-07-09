namespace BitirmeProjesi1.Data.Entities
{
    public class FlightBooking : BookingBase
    {
        public Flight Flight { get; set; }
        public FlightPayment FlightPayment { get; set; }

    }
}
