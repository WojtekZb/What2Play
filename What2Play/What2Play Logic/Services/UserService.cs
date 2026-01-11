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
            var authUser = await _repo.GetByEmailAsync(email);
            if (authUser == null) return null;

            return BCrypt.Net.BCrypt.Verify(password, authUser.hashedPassword)
                ? new User { Id = authUser.Id, email = authUser.email }
                : null;
        }
    }
}
