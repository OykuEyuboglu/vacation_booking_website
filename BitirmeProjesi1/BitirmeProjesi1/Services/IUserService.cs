using BitirmeProjesi1.Data.Entities;
using BitirmeProjesi1.DTOS;

namespace BitirmeProjesi1.Services
{
    public interface IUserService
    {
        Task<UserDTO> ValidateUserAsync(string email, string password,string? ID);
        Task<RegisterDTO> RegisterUserAsync(RegisterDTO registerDto);
        Task<CustomerProfileDTO> GetCustomerProfileAsync(string customerId);
        Task<User> FindUserByEmailPasswordAsync(string email, string password);
    }
}
