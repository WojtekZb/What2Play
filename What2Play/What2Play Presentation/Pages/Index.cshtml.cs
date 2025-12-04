using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using What2Play_Logic.Entities;
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

        public List<Game> Games { get; set; }
        public List<GameVM> GameVMList { get; set; }
        public List<GameType> TypeList { get; set; }

        [BindProperty]
        public Game NewGame { get; set; } = new();

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

            foreach (Game game in Games)
            {
                GameVM gameVM = Presentation_EntityMapper.EntityToVm(game);
                gameVMList.Add(gameVM);
            }

            TypeList = await _getTypesService.GetTypes();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Message = "Please fill in all required fields correctly.";
                return Page();
            }

            Message = await _GameService.AddGame(NewGame);
            Games = await _GameService.GetGames();
            TypeList = await _getTypesService.GetTypes(); // refresh types too
            return Page();
        }
    }
}