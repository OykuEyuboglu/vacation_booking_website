namespace BitirmeProjesi1.DTOS
{
    public class ReservationListDTO
    {
        public string PaymentMethod { get; set; }  // 'credit_card', 'bank_transfer', 'paypal'
        public string RoomType { get; set; }
        public DateTime CheckInDate { get; set; } // ArrivalDate
        public DateTime CheckOutDate { get; set; } // DepartureDate
        public string Username { get; set; }
        public string HotelName { get; set; }

    }
}
