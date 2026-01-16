using What2Play_Logic.DTOs;
using What2Play_Logic.Entities;

namespace What2Play_Logic.Interfaces
{
    public interface IUserRepo
    {
        Task CreateAccount(string email, string hash);

        Task<UserDTO> GetByEmailAsync(string email);
        List<UserDTO> GetAllUsers();
        void UpdateUserRole(int userId, string role);
    }
}
