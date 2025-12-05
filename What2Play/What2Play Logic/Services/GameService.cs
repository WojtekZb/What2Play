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

        public Task<List<GameDTO>> GetGames()
        {
            return _repo.GetGames();
        }


        public async Task<string> AddGame(GameDTO game)
        {
            Game gameEntity = Mapper.DtoToEntity(game);            
            switch (gameEntity)
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
                var gameDto = Mapper.EntityToDto(gameEntity);
                string result = await _repo.AddGame(gameDto);
                return result;
            }
        }
    }
}
