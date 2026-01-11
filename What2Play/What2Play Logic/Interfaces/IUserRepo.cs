using What2Play_Logic.DTOs;

namespace What2Play_Logic.Interfaces
{
    public interface IUserRepo
    {
        Task CreateAccount(string email, string hash);

        Task<UserDTO> GetByEmailAsync(string email);
    }
}
