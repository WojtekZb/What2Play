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

        public Task<List<GameDTO>> GetGames(int? UserId)
        {
            return _repo.GetGames(UserId);
        }

        public Task<GameDTO> GetGameById(int id, int? UserId)
        {
            return _repo.GetGameById(id, UserId);
        }


        public async Task<string> AddGame(GameDTO game, int? UserId)
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

            case { TypeName: null }:
                return "Game type can't be null";

            case { SourceName: null }:
                return "Game source can't be null";

            default:
                var gameDto = Mapper.EntityToDto(gameEntity);
                string result = await _repo.AddGame(gameDto, UserId);
                return result;
            }
        }

        public async Task<string> UpdateGame(GameDTO game, int? UserId)
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

                case { TypeName: null }:
                    return "Game type can't be null";

                case { SourceName: null }:
                    return "Game source can't be null";

                default:
                    var gameDto = Mapper.EntityToDto(gameEntity);
                    string result = await _repo.UpdateGame(gameDto, UserId);
                    return result;
            }
        }

        public async Task<string> DeleteGame(int gameId, int? UserId)
        {
            string result = await _repo.DeleteGame(gameId, UserId);
            return result;
        }
    }
}
