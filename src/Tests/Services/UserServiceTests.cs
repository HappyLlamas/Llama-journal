using DataLayer.Models;
using DataLayer.Repositories;

namespace BusinnesLayer.Services.Tests;


public class UserServiceTests
{
    private UserService UserService;
    private Mock<IUserRepository> UserRepository;

    private User TestUser = new User
    {
        Id = "AA000000",
        FullName = "Test Name",
        Email = "testmail@gmail.com",
    };

    public UserServiceTests()
    {
        this.UserRepository = new Mock<IUserRepository>();

        this.UserRepository.CallBase = true;
        this.UserRepository
            .Setup(x => x.GetUsers())
            .Returns(new List<User>() { this.TestUser });
        this.UserRepository
            .Setup(x => x.GetById(this.TestUser.Id))
            .Returns(this.TestUser);
        this.UserRepository
            .Setup(x => x.SetGroup(It.IsAny<User>(), It.IsAny<string>()));
        this.UserRepository
            .Setup(x => x.SetRole(It.IsAny<User>(), It.IsAny<RoleEnum>()));
        this.UserRepository
            .Setup(x => x.CreateUser(It.IsAny<string>(), It.IsAny<string>()))
            .Callback((string email, string fullName) =>
            {
                if (email == null || fullName == null)
                {
                    throw new ArgumentNullException();
                }
            });

        this.UserService = new UserService(
            userRepository: this.UserRepository.Object);
        return;
    }

    [Fact()]
    public void GetUsersTest()
    {
        List<User>? users = this.UserService.GetUsers();
        Assert.NotNull(users);
        return;
    }

    [Fact()]
    public void SetUserGroupTest()
    {
        Assert.Throws<Exception>(
            () => this.UserService.SetUserGroup(null!, ""));
        return;
    }

    [Fact()]
    public void SetUserRoleTest()
    {
        Assert.Throws<Exception>(
            () => this.UserService.SetUserRole(null!, RoleEnum.User));
        return;
    }

    [Fact()]
    public void CreateUserTest()
    {
        Assert.Throws<ArgumentNullException>(
            () => this.UserService.CreateUser(null!, null!));
        return;
    }
}
