using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using What2Play_Logic.Entities;
using What2Play_Logic.Services;
using What2Play_Data.Repository;
using What2Play_Presentation.ViewModels;

namespace What2Play_Presentation.Pages
{
    public class HomeModel : PageModel
    {
        private readonly AddGameService _addGameService;
        private readonly GetGameService _getGameService;
        private readonly GetTypesService _getTypesService;

        public List<Game> Games { get; set; } = new();
        public List<GameType> TypeList { get; set; } = new();

        [BindProperty]
        public Game NewGame { get; set; } = new();

        public string Message { get; set; } = string.Empty;

        public HomeModel(IConfiguration config)
        {
            // Set up your services manually here (no DI container needed)
            var getGameRepo = new GameRepo(config);
            var addGameRepo = new AddGameRepo(config);
            var getTypesRepo = new GetTypesRepo(config); // <-- you need this

            _GameService = new GameService(GameRepo);
            _addGameService = new AddGameService(addGameRepo);
            _getTypesService = new GetTypesService(getTypesRepo); // <-- initialize here
        }

        public async Task OnGet()
        {
            Games = await _GameService.GetGames();
            List<ViewModels.GameVM> gameVMs = new List<ViewModels.GameVM>();
            foreach (Game g in Games)
            {
                GameVM newGame = new GameVM();
                newGame.Name = g.Title;
                gameVMs.Add(newGame);
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

            Message = await _addGameService.AddGame(NewGame);
            Games = await _getGameService.GetGames();
            TypeList = await _getTypesService.GetTypes(); // refresh types too
            return Page();
        }
    }
}