using What2Play_Logic.Entities;
using What2Play_Logic.Interfaces;

namespace What2Play_Logic.Services
{
    public class AddGameService
    {
        private readonly IAddGameRepo _repo;

        public AddGameService(IAddGameRepo repo)
        {
            _repo = repo;
        }

        public async Task<string> AddGame(Game game)
        {
            try
            {
                // basic null check
                if (game == null)
                    throw new ArgumentNullException(nameof(game), "Game cannot be null.");

                // simple validation checks
                if (string.IsNullOrWhiteSpace(game.Title))
                    throw new ArgumentException("Game title is required.");

                // call repo
                string result = await _repo.AddGame(game);

                // optional: check repo’s response
                if (result.Contains("success", StringComparison.OrdinalIgnoreCase))
                    return result;
                else
                    return "Failed to add the game (repository did not confirm success).";
            }
            catch (ArgumentNullException ex)
            {
                return $"Invalid input: {ex.Message}";
            }
            catch (ArgumentException ex)
            {
                return $"Validation error: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Unexpected error while adding game: {ex.Message}";
            }
        }
    }
}
