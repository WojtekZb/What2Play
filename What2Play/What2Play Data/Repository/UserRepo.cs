using Microsoft.Extensions.Configuration;
using What2Play_Logic.DTOs;
using What2Play_Logic.Interfaces;
namespace What2Play_Data.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly string _connectionstring;

        public UserRepo(IConfiguration conn)
        {
            _connectionstring = conn.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string not found.");
        }

        Task CreateAccount(string email, string hash)
        {

        }

        Task<UserDTO> GetByEmailAsync(string email)
        {

        }
    }
}
