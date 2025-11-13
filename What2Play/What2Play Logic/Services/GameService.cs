using What2Play_Logic.Interfaces;
using What2Play_Logic.Entities;

namespace What2Play_Logic.Services
{
    public class GameService
    {
        private readonly IGameRepo _repo;

        public GameService(IGameRepo repo)
        {
            _repo = repo;
        }

        public Task<List<Game>> GetGames()
        {
            return _repo.GetGames();
        }
    }
}