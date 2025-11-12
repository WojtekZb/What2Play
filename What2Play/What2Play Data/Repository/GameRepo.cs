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
            string sql = "SELECT [Name], [Description] FROM Game;";
            await using var conn = new SqlConnection(_connectionstring);
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            string Title = "";
            string Description = "";

            GameList = new List<Game>();

            while (reader.Read())
            {
                Game game = new Game
                {
                    Title = reader[0].ToString(),
                    Description = reader[1].ToString()
                };
                GameList.Add(game);

            }
            conn.Close();

            return GameList;
        }
    }
}

