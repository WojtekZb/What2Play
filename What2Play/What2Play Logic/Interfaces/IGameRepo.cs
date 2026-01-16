using What2Play_Logic.DTOs;

namespace What2Play_Logic.Interfaces
{
    public interface IGameRepo
    {
        Task<List<GameDTO>> GetGames(int? UserId);
        Task<GameDTO> GetGameById(int id, int? UserId);
        Task<string> AddGame(GameDTO game, int? UserId);
        Task<string> UpdateGame(GameDTO game, int? UserId);
        Task<string> DeleteGame(int gameId, int? UserId);
    }
}
