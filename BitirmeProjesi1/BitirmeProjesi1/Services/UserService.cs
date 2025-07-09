using BitirmeProjesi1.Data.Context;
using BitirmeProjesi1.Data.Entities;
using BitirmeProjesi1.DTOS;
using Microsoft.EntityFrameworkCore;

namespace BitirmeProjesi1.Services
{
    public class UserService : IUserService
    {
        private readonly ProjectContext _db;

        public UserService(ProjectContext db)
        {
            _db = db;
        }

        public async Task<UserDTO> ValidateUserAsync(string email, string password, string? ID)
        {
            var user = await _db.Users.Where(u => u.Email == email && u.Password == password && u.ID == ID).FirstOrDefaultAsync();

            if (user == null)
                return null;

            return new UserDTO
            {
                Id = user.ID,
                Password = user.Password,
                Email = user.Email,
                UserType = user.UserType,
                FullName = $"{user.FirstName} {user.LastName}",
            };
        }

        public async Task<RegisterDTO> RegisterUserAsync(RegisterDTO registerDto)
        {
            // Kullanıcı var mı kontrolü
            var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.Email == registerDto.Email);
            if (existingUser != null)
            {
                throw new Exception("Bu e-posta ile kayıtlı bir kullanıcı zaten mevcut.");
            }

            // Yeni kullanıcı oluşturma
            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                Password = registerDto.Password,
                Address = registerDto.Address,
                City = registerDto.City,
                Country = registerDto.Country,
                PhoneNumber = registerDto.PhoneNumber,
                UserType = "Customer",
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return registerDto;
        }

        public async Task<CustomerProfileDTO> GetCustomerProfileAsync(string customerId)
        {
            var customer = await _db.Users.FindAsync(customerId);
            if (customer == null)
            {
                return null;
            }

            return new CustomerProfileDTO
            {
                Password = customer.Password,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address,
                City = customer.City,
                Country = customer.Country,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
            };

        }

        public async Task<User> FindUserByEmailPasswordAsync(string email, string password)
        {
            return await _db.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

    }
}
