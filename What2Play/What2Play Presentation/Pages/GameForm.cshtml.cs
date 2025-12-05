using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using What2Play_Data.Repository;
using What2Play_Logic.DTOs;
using What2Play_Logic.Services;

namespace What2Play_Presentation.Pages
{
    public class GameFormModel : PageModel
    {
        private readonly GameService _gameService;
        private readonly GetTypesService _getTypesService;

        public List<GameTypeDTO> TypeList { get; set; }

        [BindProperty]
        public GameDTO Game { get; set; } = new();

        public string Message { get; set; } = string.Empty;

        public bool IsEdit => Game != null && Game.Id > 0;

        public GameFormModel(IConfiguration config)
        {
            var gameRepo = new GameRepo(config);
            var getTypesRepo = new GetTypesRepo(config);

            _gameService = new GameService(gameRepo);
            _getTypesService = new GetTypesService(getTypesRepo);
        }

        // GET handler (supports both Add + Edit)
        public async Task OnGetAsync(int? id)
        {
            TypeList = await _getTypesService.GetTypes();

            if (id.HasValue)
            {
                Game = await _gameService.GetGameById(id.Value);
            }
            else
            {
                Game = new GameDTO();
            }
        }

        // POST handler
        public async Task<IActionResult> OnPostAsync()
        {
            TypeList = await _getTypesService.GetTypes(); // always load types

            if (!ModelState.IsValid)
            {
                Message = "Please fill in all required fields.";
                return Page();
            }

            if (IsEdit)
            {
                Message = await _gameService.UpdateGame(Game);
            }
            else
            {
                Message = await _gameService.AddGame(Game);
            }

            return RedirectToPage("/Index");
        }
    }
}