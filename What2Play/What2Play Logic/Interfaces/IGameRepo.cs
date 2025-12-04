using What2Play_Logic.DTOs;

namespace What2Play_Logic.Interfaces
{
    public interface IGameRepo
    {
        Task<List<GameDTO>> GetGames();
        Task<string> AddGame(GameDTO game);
    }
}
