using What2Play_Logic.DTOs;
using What2Play_Logic.Entities;

namespace What2Play_Logic.Mappers
{
    public class Mapper
    {
        public static Game DtoToEntity(GameDTO gameDTO)
        {
            return new Game
            {   
                Id = gameDTO.Id,
                Title = gameDTO.Title,
                Description = gameDTO.Description,
                TypeId = gameDTO.TypeId,
                TypeName = gameDTO.TypeName,
                SourceId = gameDTO.SourceId,
                SourceName = gameDTO.SourceName,
                Played = gameDTO.Played
            };
        }

        public static GameDTO EntityToDto(Game game)
        {
            return new GameDTO
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                TypeId = game.TypeId,
                TypeName = game.TypeName,
                SourceId = game.SourceId,
                SourceName = game.SourceName,
                Played = game.Played
            };
        }

        public static GameDTO SteamToDto(SteamGameDTO steamDTO)
        {
            return new GameDTO
            {
                Id = steamDTO.AppId,
                Title = steamDTO.Name,
                Description = steamDTO.Description,
                SourceId = steamDTO.Source,
                Played = steamDTO.PlaytimeRaw > 0
            };
        }

    }
}
