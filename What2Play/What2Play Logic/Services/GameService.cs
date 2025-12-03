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

        public async Task<string> AddGame(Game game)
        {
            switch (game)
            {
                case null:
                    return "Game can't be null";

                case { Title: null }:
                    return "Game title can't be null";

                case { Description: null }:
                    return "Game description can't be null";

                case { Type: null }:
                    return "Game type can't be null";

                case { Source: null }:
                    return "Game source can't be null";

                default:
                    string result = await _repo.AddGame(game);
                    return result;
            }
        }
    }
}
