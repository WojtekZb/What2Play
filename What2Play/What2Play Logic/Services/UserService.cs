using What2Play_Logic.Entities;
using What2Play_Logic.Interfaces;

namespace What2Play_Logic.Services
{
    public class UserService
    {
        private readonly IUserRepo _repo;

        public UserService(IUserRepo repo)
        {
            _repo = repo;
        }
        public async Task Register(string email, string password)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            await _repo.CreateAccount(email, hash);
        }

        public async Task<User?> Login(string email, string password)
        {
            var userDto = await _repo.GetByEmailAsync(email);
            if (userDto == null) return null;

            if (!BCrypt.Net.BCrypt.Verify(password, userDto.hashedPassword))
                return null;

            return new User
            {
                Id = userDto.Id,
                email = userDto.email
            };
        }
    }
}
