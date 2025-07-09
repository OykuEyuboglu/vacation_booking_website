namespace BitirmeProjesi1.Data.Entities
{
    public abstract class BookingBase : Base
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
    }
}
