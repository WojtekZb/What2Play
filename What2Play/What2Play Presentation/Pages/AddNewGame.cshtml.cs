using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using What2Play_Data.Repository;
using What2Play_Logic.DTOs;
using What2Play_Logic.Services;

namespace What2Play_Presentation.Pages
{
    public class AddNewGameModel : PageModel
    {
        private readonly GameService _GameService;
        private readonly GetTypesService _getTypesService;

        public List<GameDTO> Games { get; set; }
        public List<GameTypeDTO> TypeList { get; set; }

        [BindProperty]
        public GameDTO NewGame { get; set; } = new();

        public string Message { get; set; } = string.Empty;

        public AddNewGameModel(IConfiguration config)
        {
            var GameRepo = new GameRepo(config);
            var getTypesRepo = new GetTypesRepo(config);

            _GameService = new GameService(GameRepo);
            _getTypesService = new GetTypesService(getTypesRepo);
        }
        public async Task OnGet()
        {
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
