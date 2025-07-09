namespace BitirmeProjesi1.Data.Entities
{
    public class HotelPayment : PaymentBase
    {
        public string HotelBookingID { get; set; }
        public HotelBooking HotelBooking { get; set; }
    }
}
