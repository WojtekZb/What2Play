using What2Play_Logic.DTOs;
using What2Play_Logic.Entities;

namespace What2Play_Logic.Mappers
{
    public class Mapper
    {
        internal static Game DtoToEntity(GameDTO gameDTO)
        {
            return new Game
            {   
                Id = gameDTO.Id,
                Title = gameDTO.Title,
                Description = gameDTO.Description,
                Type = gameDTO.Type,
                Source = gameDTO.Source,
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
                Type = game.Type,
                Source = game.Source,
                Played = game.Played
            };
        }

    }
}
