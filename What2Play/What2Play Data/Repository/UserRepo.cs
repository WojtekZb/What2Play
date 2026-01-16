using Microsoft.Data.SqlClient;
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
            _connectionstring = conn.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string not found.");
        }

        public async Task CreateAccount(string email, string hash)
        {
            const string sql = """
                INSERT INTO [User] (Email, Password)
                VALUES (@Email, @Password)
            """;

            using var conn = new SqlConnection(_connectionstring);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", hash);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<UserDTO?> GetByEmailAsync(string email)
        {
            const string sql = """
                SELECT UserId, Email, Password, Role
                FROM [User]
                WHERE Email = @Email
            """;

            using var conn = new SqlConnection(_connectionstring);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Email", email);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
                return null;

            return new UserDTO
            {
                Id = reader.GetInt32(0),
                email = reader.GetString(1),
                hashedPassword = reader.GetString(2),
                role = reader.GetString(3)
            };
        }

        public List<UserDTO> GetAllUsers()
        {
            var users = new List<UserDTO>();
            const string sql = "SELECT UserId, Email, Role FROM [User]";

            using var conn = new SqlConnection(_connectionstring);
            conn.Open();

            using var cmd = new SqlCommand(sql,conn);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new UserDTO
                {
                    Id = reader.GetInt32(0),
                    email = reader.GetString(1),
                    role = reader.GetString(2)
                });
            }

            return users;
        }

        public void UpdateUserRole(int userId, string role)
        {
            const string sql = "UPDATE [User] SET Role = @Role WHERE UserId = @UserId";
            using var conn = new SqlConnection(_connectionstring);
            conn.Open();

            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Role", role);
            cmd.Parameters.AddWithValue("@UserId", userId);

            cmd.ExecuteNonQuery();
        }
    }
}
