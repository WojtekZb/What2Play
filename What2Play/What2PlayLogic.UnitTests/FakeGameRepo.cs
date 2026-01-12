using What2Play_Logic.Interfaces;
using What2Play_Logic.DTOs;

public class FakeGameRepo : IGameRepo
{
    public bool AddCalled;

    public Task<List<GameDTO>> GetGames()
    {
        return null;
    }

    public Task<GameDTO> GetGameById(int id)
    {
        return null;
    }

    public Task<string> AddGame(GameDTO game)
    {
        AddCalled = true;
        return Task.FromResult($"Game {game.Title} added successfully.");
    }

    public Task<string> UpdateGame(GameDTO game)
    {
        return null;
    }

    public Task<string> DeleteGame(int gameId)
    {
        return null;
    }
}
