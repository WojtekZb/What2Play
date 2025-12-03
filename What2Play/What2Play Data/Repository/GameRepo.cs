using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using What2Play_Logic.Entities;
using What2Play_Logic.Interfaces;
using What2Play_Data.Mappers;

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
        public List<Game> GameList { get; set; }

        public async Task<List<Game>> GetGames()
        {
            string sql = @"SELECT g.name, g.description, gt.type AS type
                   FROM Game g 
                   JOIN GameType gt ON g.TypeId = gt.TypeId";

            await using var conn = new SqlConnection(_connectionstring);
            await conn.OpenAsync();

            using var cmd = new SqlCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            var gameDTOList = new List<GameDTO>();
            var gameList = new List<Game>();

            while (await reader.ReadAsync())
            {
                var game = new GameDTO
                {
                    Title = reader[0].ToString(),
                    Description = reader[1].ToString(),
                    Type = reader[2].ToString()
                };
                gameDTOList.Add(game);
            }

            foreach (var game in gameDTOList)
            {
                var gameE = Data_EntityMapper.DtoToEntity(game);
                if (gameE != null)
                    gameList.Add(gameE);
            }

            return gameList;
        }


        public async Task<string> AddGame(Game gameE)
        {
            await using var conn = new SqlConnection(_connectionstring);
            await conn.OpenAsync();

            GameDTO game = Data_EntityMapper.EntityToDto(gameE);  

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
