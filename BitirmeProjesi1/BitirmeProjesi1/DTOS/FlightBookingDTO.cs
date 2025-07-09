namespace BitirmeProjesi1.DTOS
{
    public class FlightBookingDTO
    {
        public string FlightID { get; set; }
        public string UserID { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string PaymentMethod { get; set; }  // 'credit_card', 'bank_transfer', 'paypal'
        public int PassengerCount { get; set; }

    }
}