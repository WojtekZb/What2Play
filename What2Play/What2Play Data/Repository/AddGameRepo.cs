using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using What2Play_Logic.Entities;
using What2Play_Logic.Interfaces;


namespace What2Play_Data.Repository
{
    public class AddGameRepo : IAddGameRepo
    {
        private readonly string _connectionstring;

        public AddGameRepo(IConfiguration conn)
        {
            _connectionstring = conn.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string not found.");
        }

        public async Task<string> AddGame(Game game)
        {
            await using var conn = new SqlConnection(_connectionstring);
            await conn.OpenAsync();

            string query = @"
                INSERT INTO Game (Name, Description, TypeId, SourceId, Played)
                VALUES (@Title, @Description, @Type, @Source, @Played);";

            await using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Title", game.Title);
                cmd.Parameters.AddWithValue("@Description", game.Description);
                cmd.Parameters.AddWithValue("@Type", game.Type);
                cmd.Parameters.AddWithValue("@Source", game.Source);
                cmd.Parameters.AddWithValue("@Played", game.Played);

                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                if (rowsAffected > 0)
                    return $"Game {game.Title} added successfully.";
                else
                    return "Failed to add game.";
            }

        }
    }
}
