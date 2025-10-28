using What2Play_Logic.Interfaces;
using What2Play_Logic.Entities;
using Microsoft.Data.SqlClient;


namespace What2Play_Data.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly string _connectionString;

        public GameRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Game> GetAllGames()
        {
            var games = new List<Game>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Game", conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        games.Add(new Game
                        {
                            Title = reader["Name"].ToString(),
                            Description = reader["Description"].ToString()
                        });
                    }
                }
            }

            return games;
        }
    }

}
