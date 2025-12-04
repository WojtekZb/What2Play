using What2Play_Logic.Interfaces;
using What2Play_Logic.Entities;
using What2Play_Logic.DTOs;
using What2Play_Logic.Mappers;

namespace What2Play_Logic.Services
{
    public class GameService
    {
        private readonly IGameRepo _repo;

        public GameService(IGameRepo repo)
        {
            _repo = repo;
        }

        List<Game> games = new List<Game>();

        public async Task<List<Game>> GetGames()
        {
            var gameDtos = await _repo.GetGames();
            var games = new List<Game>();
            foreach (GameDTO g in gameDtos)
            {
                games.Add(Mapper.DtoToEntity(g));
            }
            return games;
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
                    var gameDto = Mapper.EntityToDto(game);
                    string result = await _repo.AddGame(gameDto);
                    return result;
            }
        }
    }
}
