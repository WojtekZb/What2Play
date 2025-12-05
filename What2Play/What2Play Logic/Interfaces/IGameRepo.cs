using What2Play_Logic.DTOs;

namespace What2Play_Logic.Interfaces
{
    public interface IGameRepo
    {
        Task<List<GameDTO>> GetGames();
        Task<GameDTO> GetGameById(int id);
        Task<string> AddGame(GameDTO game);
        Task<string> UpdateGame(GameDTO game);
        Task<string> DeleteGame(int gameId);
    }
}
