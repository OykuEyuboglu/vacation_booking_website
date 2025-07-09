namespace BitirmeProjesi1.Data.Entities
{
    public class HotelBooking : BookingBase
    {
        public string RoomID { get; set; }
        public Room Room { get; set; }
        public HotelPayment HotelPayment { get; set; }
    }
}
