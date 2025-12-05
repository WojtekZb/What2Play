using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using What2Play_Logic.DTOs;
using What2Play_Logic.Services;
using What2Play_Data.Repository;
using What2Play_Presentation.ViewModels;
using What2Play_Presentation.Mappers;

namespace What2Play_Presentation.Pages
{
    public class HomeModel : PageModel
    {
        private readonly GameService _GameService;
        private readonly GetTypesService _getTypesService;

        public List<GameDTO> Games { get; set; }
        public List<GameVM> GameVMList { get; set; }
        public List<GameTypeDTO> TypeList { get; set; }

        [BindProperty]
        public GameDTO NewGame { get; set; } = new();

        public string Message { get; set; } = string.Empty;

        public HomeModel(IConfiguration config)
        {
            var GameRepo = new GameRepo(config);
            var getTypesRepo = new GetTypesRepo(config);

            _GameService = new GameService(GameRepo);
            _getTypesService = new GetTypesService(getTypesRepo);
        }

        public async Task OnGet()
        {
            var gameVMList = new List<GameVM>();
            Games = await _GameService.GetGames();

            foreach (GameDTO game in Games)
            {
                GameVM gameVM = Mapper.DtoToVm(game);
                gameVMList.Add(gameVM);
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _GameService.DeleteGame(id);

            // Reload the game list after deletion
            Games = await _GameService.GetGames();

            return Page(); // stay on the same page
        }
    }
}