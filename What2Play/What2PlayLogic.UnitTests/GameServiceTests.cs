using What2Play_Logic.DTOs;
using What2Play_Logic.Services;

namespace What2PlayLogic.UnitTests
{
    [TestClass]
    public class GameServiceTests
    {
        private FakeGameRepo _fakeRepo;
        private GameService _service;

        [TestInitialize]
        public void Setup()
        {
            _fakeRepo = new FakeGameRepo();
            _service = new GameService(_fakeRepo);
        }

        [TestMethod]
        public async Task AddGame_ValidGame_ReturnsSuccessAndCallsRepo()
        {
            // Arrange
            var game = new GameDTO
            {
                Title = "Test Game",
                Description = "Fun game",
                TypeName = "RPG",
                SourceName = "Steam"
            };

            // Act
            var result = await _service.AddGame(game, 4);

            // Assert
            Assert.AreEqual("Game added", result);
            Assert.IsTrue(_fakeRepo.AddCalled);
        }

        [TestMethod]
        public async Task AddGame_NullTitle_ReturnsError()
        {
            var game = new GameDTO
            {
                Title = null,
                Description = "Desc",
                TypeName = "RPG",
                SourceName = "Steam"
            };

            var result = await _service.AddGame(game, 4);

            Assert.AreEqual("Game title can't be null", result);
            Assert.IsFalse(_fakeRepo.AddCalled);
        }

        [TestMethod]
        public async Task UpdateGame_ValidGame_ReturnsSuccessAndCallsRepo()
        {
            var game = new GameDTO
            {
                Id = 1,
                Title = "Sekiro",
                Description = "Fun game",
                TypeName = "RPG",
                SourceName = "Steam"
            };

            var result = await _service.UpdateGame(game, 4);

            Assert.AreEqual($"{game.Title} updated", result);
            Assert.IsTrue(_fakeRepo.UpdateCalled);
        }

        [TestMethod]
        public async Task UpdateGame_NullDescription_ReturnsError()
        {
            var game = new GameDTO
            {
                Id = 1,
                Title = "Updated Game",
                Description = null,
                TypeName = "RPG",
                SourceName = "Steam"
            };

            var result = await _service.UpdateGame(game, 4);

            Assert.AreEqual("Game description can't be null", result);
            Assert.IsFalse(_fakeRepo.UpdateCalled);
        }

        [TestMethod]
        public async Task DeleteGame_ValidId_ReturnsSuccessAndCallsRepo()
        {
            var result = await _service.DeleteGame(1, 4);

            Assert.AreEqual("Game removed", result);
            Assert.IsTrue(_fakeRepo.DeleteCalled);
        }

        [TestMethod]
        public async Task GetGames_ValidUser_ReturnsList()
        {
            var result = await _service.GetGames(4);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public async Task GetGameById_ValidId_ReturnsGame()
        {
            var result = await _service.GetGameById(1, 4);

            Assert.IsNotNull(result);
            Assert.AreEqual("Test Game", result.Title);
        }

        [TestMethod]
        public async Task GetGameById_InvalidId_ReturnsNull()
        {
            var result = await _service.GetGameById(-1, 4);

            Assert.IsNull(result);
        }

    }
}