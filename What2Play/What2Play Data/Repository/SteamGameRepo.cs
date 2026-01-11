using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using What2Play_Logic.DTOs;
using What2Play_Logic.Interfaces;
using What2Play_Logic.Mappers;

namespace What2Play_Data.Repository
{
    public class SteamGameRepo : ISteamGamesRepo
    {
        private readonly string _connectionstring;
        private readonly HttpClient _http;

        public SteamGameRepo(HttpClient http, IConfiguration conn)
        {
            _http = http;
            _connectionstring = conn.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string not found.");
        }
        public async Task<string> AddSteamGames(string steamId)
        {
            string apiKey = "227BE23A23CF909A2CE2F9300E9445CC";
            var ownedJson = await _http.GetStringAsync(
            $"https://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={apiKey}&steamid={steamId}&format=json&include_appinfo=true"
);

            // 2. Parse the JSON to get the games array
            using var doc = JsonDocument.Parse(ownedJson);
            var gamesArray = doc.RootElement.GetProperty("response").GetProperty("games");  

            // 3. Parallel fetch metadata and insert into DB
            var tasks = gamesArray.EnumerateArray().Select(async g =>
            {
                var steamGame = new SteamGameDTO
                {
                    AppId = g.GetProperty("appid").GetInt32(),
                    Name = g.GetProperty("name").GetString() ?? "",
                    Played = g.GetProperty("playtime_forever").GetInt32() > 0
                };

                // 3a. Fetch full metadata from appdetails API
                var detailsJson = await _http.GetStringAsync(
         $"https://store.steampowered.com/api/appdetails?appids={steamGame.AppId}"
     );

                using var detailsDoc = JsonDocument.Parse(detailsJson);
                var data = detailsDoc.RootElement
                                     .GetProperty(steamGame.AppId.ToString())
                                     .GetProperty("data");

                steamGame.Description = data.GetProperty("short_description").GetString() ?? "";

                GameDTO gameDTO = Mapper.SteamToDto(steamGame);

                // Insert into DB
                using var conn = new SqlConnection(_connectionstring);
                await conn.OpenAsync();

                string query = @"
                INSERT INTO Game (GameId, TypeId, Name, Description)
                VALUES (@GameId, 1, @Title, @Description);

                INSERT INTO UserGame (UserId, GameId, SourceId, Played)
                VALUES (1, @GameId, 2, @Played);";

                await using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@GameId", gameDTO.Id);
                    cmd.Parameters.AddWithValue("@Title", gameDTO.Title);
                    cmd.Parameters.AddWithValue("@Description", gameDTO.Description);
                    cmd.Parameters.AddWithValue("@Played", gameDTO.Played);

                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    if (rowsAffected > 0)
                        return $"Game {gameDTO.Title} added successfully.";
                    else
                        return "Failed to add game.";
                }
            });

            // 4. Wait for all tasks to finish
            await Task.WhenAll(tasks);
            return "Steam Games added";
        }
    }
}
