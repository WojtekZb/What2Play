namespace What2Play_Logic.Interfaces
{
    public interface ISteamGamesRepo
    {
        Task<string> AddSteamGames(string steamId);
    }
}
