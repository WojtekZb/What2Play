using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using What2Play_Logic.Entities;
using What2Play_Logic.Services;
using What2Play_Data.Repository;

namespace What2Play_Presentation.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AddGameService _addGameService;
        private readonly GameService _gameService;

        public List<Game> Games { get; set; } = new();
        [BindProperty]
        public Game NewGame { get; set; } = new();
        public string Message { get; set; } = string.Empty;

        public IndexModel(IConfiguration config)
        {
            // Set up your services manually here (no DI container needed)
            var repo = new GameRepo(config);
            _gameService = new GameService(repo);
            _addGameService = new AddGameService(new AddGameRepo(config));
        }

        public async Task OnGet()
        {
            Games = await _gameService.GetGames();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Message = "Please fill in all required fields correctly.";
                return Page();
            }

            Message = await _addGameService.AddGame(NewGame);
            Games = await _gameService.GetGames();
            return Page();
        }
    }
}
