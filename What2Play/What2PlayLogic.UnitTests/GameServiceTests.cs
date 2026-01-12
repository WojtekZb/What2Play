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
            var result = await _service.AddGame(game);

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

            var result = await _service.AddGame(game);

            Assert.AreEqual("Game title can't be null", result);
            Assert.IsFalse(_fakeRepo.AddCalled);
        }
    }
}