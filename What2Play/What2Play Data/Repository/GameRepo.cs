using What2Play_Logic.Interfaces;
using What2Play_Logic.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


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
        public List<Game> GameList { get; set; }


        public async Task<List<Game>> GetGames()
        {
            string sql = @"SELECT g.name, g.description, gt.type AS type, gs.source AS source, g.played 
                           FROM Game g 
                           JOIN GameType gt ON g.TypeId = gt.TypeId 
                           JOIN GameSource gs ON g.SourceId = gs.SourceId;";
            await using var conn = new SqlConnection(_connectionstring);
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            string Title = "";
            string Description = "";
            string Type = "";
            string Source = "";
            bool Played;

            GameList = new List<Game>();

            while (reader.Read())
            {
                Game game = new Game
                {
                    Title = reader[0].ToString(),
                    Description = reader[1].ToString(),
                    Type = reader[2].ToString(),
                    Source = reader[3].ToString(),
                    Played = Convert.ToBoolean(reader[4])
                };
                GameList.Add(game);

            }
            conn.Close();

            return GameList;
        }
    }
}

