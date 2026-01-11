using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using What2Play_Logic.DTOs;
using What2Play_Logic.Interfaces;

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
            string sql = @"SELECT 
                           g.GameId,
                           g.[Name],
                           g.[Description],
                           t.[Type] AS type,
                           ug.SourceId,
                           ug.Played
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
                    Id = (int)reader[0],
                    Title = reader[1].ToString(),
                    Description = reader[2].ToString(),
                    TypeName = reader[3].ToString(),
                    SourceName = reader[4].ToString(),
                    Played = (bool)reader[5]
                };
                gameList.Add(game);
            }

            return gameList;
        }

        public async Task<GameDTO> GetGameById(int id)
        {
            await using var conn = new SqlConnection(_connectionstring);
            await conn.OpenAsync();

            string query = @"
                            SELECT 
                                g.GameId,
                                g.Name AS Title,
                                g.Description,
                                g.TypeId AS Type,
                                ug.SourceId AS Source,
                                ug.Played
                            FROM Game g
                            INNER JOIN UserGame ug ON g.GameId = ug.GameId
                            WHERE g.GameId = @GameId AND ug.UserId = 1;
                        ";

            await using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@GameId", id);

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new GameDTO
                {
                    Id = (int)reader[0],
                    Title = reader[1].ToString(),
                    Description = reader[2].ToString(),
                    TypeName = reader[3].ToString(),
                    SourceName = reader[4].ToString(),
                    Played = (bool)reader[5]
                };
            }

            return null;
        }


        public async Task<string> AddGame(GameDTO game)
        {
            await using var conn = new SqlConnection(_connectionstring);
            await conn.OpenAsync();  

            string query = @"
                INSERT INTO Game (Name, Description, TypeId)
                VALUES (@Title, @Description, @Type);

                INSERT INTO UserGame (UserId, GameId, SourceId, Played)
                VALUES (1, SCOPE_IDENTITY(), 1, @Played);";

            await using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Title", game.Title);
                cmd.Parameters.AddWithValue("@Description", game.Description);
                cmd.Parameters.AddWithValue("@Type", game.TypeId);
                cmd.Parameters.AddWithValue("@Played", game.Played);

                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                if (rowsAffected > 0)
                    return $"Game {game.Title} added successfully.";
                else
                    return "Failed to add game.";
            }
        }

        public async Task<string> UpdateGame(GameDTO game)
        {
            await using var conn = new SqlConnection(_connectionstring);
            await conn.OpenAsync();

            string query = @"
                            UPDATE Game
                            SET 
                                Name = @Title,
                                Description = @Description,
                                TypeId = @Type
                            WHERE GameId = @GameId;

                            UPDATE UserGame
                            SET 
                                SourceId = @SourceId,
                                Played = @Played
                            WHERE GameId = @GameId AND UserId = 1;";

            await using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@GameId", game.Id);
                cmd.Parameters.AddWithValue("@Title", game.Title);
                cmd.Parameters.AddWithValue("@Description", game.Description);
                cmd.Parameters.AddWithValue("@Type", game.TypeId);
                cmd.Parameters.AddWithValue("@SourceId", game.SourceId);
                cmd.Parameters.AddWithValue("@Played", game.Played);

                int rows = await cmd.ExecuteNonQueryAsync();

                if (rows > 0)
                    return $"Game {game.Title} updated.";
                else
                    return "Update failed.";
            }
        }

        public async Task<string> DeleteGame(int gameId)
        {
            await using var conn = new SqlConnection(_connectionstring);
            await conn.OpenAsync();

            string query = @"
                            DELETE FROM UserGame
                            WHERE GameId = @GameId AND UserId = 1;";

            await using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@GameId", gameId);

                int rows = await cmd.ExecuteNonQueryAsync();

                if (rows > 0)
                    return $"Game removed.";
                else
                    return "Update failed.";
            }
        }
    }
}
