using What2Play_Logic.Entities;
using What2Play_Logic.Interfaces;

namespace What2Play_Logic.Services
{
    public class AddGameService
    {
        private readonly IAddGameRepo _repo;

        public AddGameService(IAddGameRepo repo)
        {
            _repo = repo;
        }

        public async Task<string> AddGame(Game game)
        {
            //if (game == null)
            //{
            //    return "Game can't be empty";
            //}
            //if (game.Title == null)
            //{
            //    return "Game Title can't be empty";
            //}
            //if (game.Description == null)
            //{
            //    return "Game Description can't be empty";
            //}
            //if (game.Type == null)
            //{
            //    return "Game Type can't be empty";
            //}
            //if (game.Source == null)
            //{
            //    return "Game Source can't be empty";
            //}
            //else
            //{
            //    string result = await _repo.AddGame(game);
            //    return result;
            //}
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
