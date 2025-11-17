using What2Play_Logic.Interfaces;
using What2Play_Logic.Entities;

namespace What2Play_Logic.Services
{
    public class GetGameService
    {
        private readonly IGetGameRepo _repo;

        public GetGameService(IGetGameRepo repo)
        {
            _repo = repo;
        }

        public Task<List<Game>> GetGames()
        {
            return _repo.GetGames();
        }
    }
}