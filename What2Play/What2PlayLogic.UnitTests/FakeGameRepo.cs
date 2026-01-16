using What2Play_Logic.DTOs;
using What2Play_Logic.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

public class FakeGameRepo : IGameRepo
{
    public bool AddCalled;
    public bool UpdateCalled;
    public bool DeleteCalled;

    public Task<List<GameDTO>> GetGames(int? userId)
    {
        return Task.FromResult(new List<GameDTO> { new GameDTO { Id = 1, Title = "Test Game" } });
    }

    public Task<GameDTO> GetGameById(int id, int? userId)
    {
        if (id <= 0) return Task.FromResult<GameDTO>(null);
        return Task.FromResult(new GameDTO { Id = id, Title = "Test Game" });
    }

    public Task<string> AddGame(GameDTO game, int? userId)
    {
        AddCalled = true;
        return Task.FromResult("Game added");
    }

    public Task<string> UpdateGame(GameDTO game, int? userId)
    {
        UpdateCalled = true;
        return Task.FromResult($"{game.Title} updated");
    }

    public Task<string> DeleteGame(int gameId, int? userId)
    {
        DeleteCalled = true;
        return Task.FromResult("Game removed");
    }
}
