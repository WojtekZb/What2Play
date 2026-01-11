using What2Play_Logic.DTOs;
using What2Play_Presentation.ViewModels;

namespace What2Play_Presentation.Mappers
{
    public class Mapper
    {
        public static GameDTO VmToDto(GameVM gameVM)
        {
            return new GameDTO
            {
                Title = gameVM.Title,
                Description = gameVM.Description,
                TypeName = gameVM.Type,
                SourceName = gameVM.Source,
                Played = gameVM.Played
            };
        }

        public static GameVM DtoToVm(GameDTO game)
        {
            return new GameVM
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                Type = game.TypeName,
                Source = game.SourceName,
                Played = game.Played
            };
        }
    }
}
