using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using What2Play_Logic.Entities;
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
        public List<GameDTO> GameList { get; set; }


        public async Task<List<GameDTO>> GetGames()
        {
            string sql = @"SELECT g.name, g.description, gt.type AS type
                           FROM Game g 
                           JOIN GameType gt ON g.TypeId = gt.TypeId";
            await using var conn = new SqlConnection(_connectionstring);
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            string Title = "";
            string Description = "";
            string Type = "";

            GameList = new List<GameDTO>();

            while (reader.Read())
            {
                GameDTO game = new GameDTO
                {
                    Title = reader[0].ToString(),
                    Description = reader[1].ToString(),
                    Type = reader[2].ToString()
                };
                GameList.Add(game);

            }
            conn.Close();

            return GameList;
        }

        public async Task<string> AddGame(GameDTO game)
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
