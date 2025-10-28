using What2Play_Logic.Entities;

namespace What2Play_Logic.Interfaces
{
    public interface IGameService
    {
        public IEnumerable<Game> GetAllGames();
    }
}
