namespace BitirmeProjesi1.DTOS
{
    public class CustomerProfileDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public string GetFullName() // Method to get full name
        {
            return $"{FirstName} {LastName}";
        }
    }
}
