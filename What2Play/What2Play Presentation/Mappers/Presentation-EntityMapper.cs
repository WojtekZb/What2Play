using What2Play_Logic.Entities;
using What2Play_Presentation.ViewModels;

namespace What2Play_Presentation.Mappers
{
    public class Presentation_EntityMapper
    {
        public static Game VmToEntity(GameVM gameVM)
        {
            return new Game
            {
                Title = gameVM.Title,
                Description = gameVM.Description,
                Type = gameVM.Type,
                Source = gameVM.Source,
                Played = gameVM.Played
            };
        }

        public static GameVM EntityToVm(Game game)
        {
            return new GameVM
            {
                Title = game.Title,
                Description = game.Description,
                Type = game.Type,
                Source = game.Source,
                Played = game.Played
            };
        }
    }
}
