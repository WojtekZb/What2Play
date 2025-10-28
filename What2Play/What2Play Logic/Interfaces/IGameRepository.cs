using What2Play_Logic.Entities;

namespace What2Play_Logic.Interfaces
{
    public interface IGameRepository
    {
        public IEnumerable<Game> GetAllGames();
    }
}
