using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using What2Play_Logic.Interfaces;
using What2Play_Logic.DTOs;

namespace What2Play_Data.Repository
{
    public class GameRepo : IGameRepo
    {
        private readonly string _connectionstring;

        public GameRepo(IConfiguration conn)
        {
            _connectionstring = conn.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string not found.");
        }
        public List<GameDTO> GameDTOList { get; set; }

        public async Task<List<GameDTO>> GetGames()
        {
            string sql = @"SELECT g.[Name],
                           g.[Description],
                           t.[Type] AS type
                           FROM UserGame ug
                           JOIN Game g ON ug.GameId = g.GameId
                           JOIN GameType t ON g.TypeId = t.TypeId
                           WHERE ug.UserId = 1;";
            await using var conn = new SqlConnection(_connectionstring);
            await conn.OpenAsync();

            using var cmd = new SqlCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            var gameList = new List<GameDTO>();

            while (await reader.ReadAsync())
            {
                var game = new GameDTO
                {
                    Title = reader[0].ToString(),
                    Description = reader[1].ToString(),
                    Type = reader[2].ToString()
                };
                gameList.Add(game);
            }

            return gameList;
        }


        public async Task<string> AddGame(GameDTO game)
        {
            await using var conn = new SqlConnection(_connectionstring);
            await conn.OpenAsync();  

            string query = @"
                INSERT INTO Game (Name, Description, TypeId)
                VALUES (@Title, @Description, @Type);

                INSERT INTO UserGame (UserId, GameId)
                VALUES (1, SCOPE_IDENTITY());";

            await using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Title", game.Title);
                cmd.Parameters.AddWithValue("@Description", game.Description);
                cmd.Parameters.AddWithValue("@Type", game.Type);

                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                if (rowsAffected > 0)
                    return $"Game {game.Title} added successfully.";
                else
                    return "Failed to add game.";
            }

        }
    }
}
