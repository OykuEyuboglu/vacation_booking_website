namespace BitirmeProjesi1.DTOS
{
    public class HotelBookingDTO
    {
        public string RoomID { get; set; }
        public string UserID { get; set; }
        public string PaymentMethod { get; set; }  // 'credit_card', 'bank_transfer', 'paypal'

        public string RoomType { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

    }
}
