using Microsoft.AspNetCore.Mvc.RazorPages;
using What2Play_Logic.Entities;
using What2Play_Logic.Services;
using What2Play_Data.Repository;

namespace What2Play_Presentation.Pages
{
    public class IndexModel : PageModel
    {

        private readonly GameService _gameService;
        public List<Game> Games { get; set; }

        public IndexModel(IConfiguration config)
        {
            _gameService = new GameService(new GameRepo(config));
        }


        public async void OnGet()
        {
            Games = await _gameService.GetGames();
        }
    }
}