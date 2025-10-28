using Microsoft.AspNetCore.Mvc.RazorPages;
using What2Play_Logic.Interfaces;
using What2Play_Logic.Entities;
using What2Play_Logic.Services;
using What2Play_Data.Repository;

namespace What2Play_Presentation.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IGameService _gameService;

        public IndexModel(ILogger<IndexModel> logger, IGameService gameService)
        {
            _logger = logger;
            _gameService = gameService;
        }

        public IEnumerable<Game> Games { get; set; }

        public void OnGet()
        {
            Games = _gameService.GetAllGames();
            ViewData["Games"] = Games;

        }
    }
}