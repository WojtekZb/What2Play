using What2Play_Logic.DTOs;
using What2Play_Logic.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

public class FakeUserRepo : IUserRepo
{
    public bool CreateAccountCalled;
    public bool UpdateUserRoleCalled;

    public Task CreateAccount(string email, string hash)
    {
        CreateAccountCalled = true;
        return Task.CompletedTask;
    }

    public Task<UserDTO> GetByEmailAsync(string email)
    {
        if (email == "notfound@example.com") return Task.FromResult<UserDTO>(null!);
        return Task.FromResult(new UserDTO
        {
            Id = 1,
            email = email,
            hashedPassword = BCrypt.Net.BCrypt.HashPassword("Valid123!"),
            role = "User"
        });
    }

    public List<UserDTO> GetAllUsers()
    {
        return new List<UserDTO>
        {
            new UserDTO { Id = 1, email = "a@example.com", role = "User" },
            new UserDTO { Id = 2, email = "b@example.com", role = "Admin" }
        };
    }

    public void UpdateUserRole(int userId, string role)
    {
        UpdateUserRoleCalled = true;
    }
}
