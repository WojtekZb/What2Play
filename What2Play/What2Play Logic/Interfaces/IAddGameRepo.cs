using What2Play_Logic.Entities;

namespace What2Play_Logic.Interfaces
{
    public interface IAddGameRepo
    {
        Task<string> AddGame(Game game);
    }
}
