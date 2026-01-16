using What2Play_Logic.Entities;
using What2Play_Logic.Interfaces;
using What2Play_Logic.DTOs;

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
            var validation = password switch
            {
                string s when s.Length < 8 => "Password must be at least 8 characters.",
                string s when !s.Any(char.IsUpper) => "Password must contain at least one uppercase letter.",
                string s when !s.Any(char.IsLower) => "Password must contain at least one lowercase letter.",
                string s when !s.Any(char.IsDigit) => "Password must contain at least one number.",
                string s when !s.Any(ch => !char.IsLetterOrDigit(ch)) => "Password must contain at least one special character.",
                _ => null
            };

            if (validation != null)
                throw new ArgumentException(validation);

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
                email = userDto.email,
                role = userDto.role
            };
        }

        public List<UserDTO> GetAllUsersWithRoles()
        {
            return _repo.GetAllUsers()
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    role = u.role
                })
                .ToList();
        }

        public void UpdateUserRoles(
            List<UserDTO> users,
            int actingUserId)
        {
            foreach (var user in users)
            {
                // optional safety: prevent self-demotion
                if (user.Id == actingUserId && user.role != "Admin")
                    continue;

                _repo.UpdateUserRole(user.Id, user.role);
            }
        }
    }
}
