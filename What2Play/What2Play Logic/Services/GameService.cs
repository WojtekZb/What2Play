using What2Play_Logic.Interfaces;
using What2Play_Logic.Entities;

namespace What2Play_Logic.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repo;

        public GameService(IGameRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _repo.GetAllGames();
        }
    }
}
