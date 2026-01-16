using What2Play_Logic.DTOs;
using What2Play_Logic.Services;

namespace What2PlayLogic.UnitTests;

[TestClass]
public class UserServiceTest
{
    private UserService _service;
    private FakeUserRepo _fakeRepo;

    [TestInitialize]
    public void Setup()
    {
        _fakeRepo = new FakeUserRepo();
        _service = new UserService(_fakeRepo);
    }

    [TestMethod]
    public async Task Register_ValidPassword_CreatesAccount()
    {
        var email = "user@example.com";
        var password = "Valid123!";

        await _service.Register(email, password);

        Assert.IsTrue(_fakeRepo.CreateAccountCalled);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task Register_ShortPassword_ThrowsException()
    {
        var email = "user@example.com";
        var password = "short";

        await _service.Register(email, password);
    }

    [TestMethod]
    public async Task Login_ValidCredentials_ReturnsUser()
    {
        var result = await _service.Login("test@example.com", "Valid123!");

        Assert.IsNotNull(result);
        Assert.AreEqual("User", result.role);
    }

    [TestMethod]
    public async Task Login_InvalidPassword_ReturnsNull()
    {
        var result = await _service.Login("test@example.com", "WrongPass1!");

        Assert.IsNull(result);
    }

    [TestMethod]
    public void GetAllUsersWithRoles_ReturnsList()
    {
        var result = _service.GetAllUsersWithRoles();

        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(result.Any(u => u.role == "Admin"));
    }

    [TestMethod]
    public void UpdateUserRoles_ValidUsers_CallsRepo()
    {
        var users = new List<UserDTO>
        {
            new UserDTO { Id = 1, role = "Admin" },
            new UserDTO { Id = 2, role = "User" }
        };

        _service.UpdateUserRoles(users, actingUserId: 3);

        Assert.IsTrue(_fakeRepo.UpdateUserRoleCalled);
    }

    [TestMethod]
    public void UpdateUserRoles_ActingUserCannotDemoteSelf()
    {
        var users = new List<UserDTO>
        {
            new UserDTO { Id = 1, role = "User" }
        };

        _service.UpdateUserRoles(users, actingUserId: 1);

        Assert.IsFalse(_fakeRepo.UpdateUserRoleCalled);
    }
}
