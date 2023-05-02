using Xunit;
using BusinnesLayer.Services;
using DataLayer.Models;
using DataLayer.Repositories;

namespace BusinnesLayer.Services.Tests;


public class UserServiceTests
{
    private UserService UserService;
    private Mock<IUserRepository> UserRepository;
    private Mock<IGroupRepository> GroupRepository;

    private User TestUser = new User
    {
        Id = "AA000000",
        FullName = "Test Name",
        Email = "testmail@gmail.com",
    };

    public UserServiceTests()
    {
        this.UserRepository = new Mock<IUserRepository>();
        this.SetupUserRepository();

        this.GroupRepository = new Mock<IGroupRepository>();
        this.SetupGroupRepository();

        this.UserService = new UserService(
            userRepository: this.UserRepository.Object,
            groupRepository: this.GroupRepository.Object);
        return;
    }

    private void SetupUserRepository()
    {
        this.UserRepository.CallBase = true;
        this.UserRepository
            .Setup(x => x.GetUsers())
            .Returns(Task.FromResult<List<User>>(new List<User>() { this.TestUser }));
        this.UserRepository
            .Setup(x => x.GetById(this.TestUser.Id))
            .Returns(Task.FromResult<User?>(this.TestUser));
        this.UserRepository
            .Setup(x => x.GetById(this.TestUser.Id))
            .Returns(Task.FromResult<User?>(this.TestUser));
        this.UserRepository
            .Setup(x => x.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RoleEnum>()))
            .Callback((string email, string fullName, RoleEnum role) =>
            {
                if (email == null || fullName == null)
                {
                    throw new ArgumentNullException();
                }
            });
        return;
    }

    private void SetupGroupRepository()
    {
        return;
    }

    [Fact()]
    public async void GetUsersTest_ReturnsEmptyList()
    {
        List<User>? users = await this.UserService.GetUsers();
        Assert.NotNull(users);
        return;
    }

    [Fact()]
    public async void SetUserGroupTest_EmptyUser_ThrowsException()
    {
        await Assert.ThrowsAsync<Exception>(
            () => this.UserService.SetUserGroup(
                userId: null!, 
                groupId: 0));
        return;
    }

    [Fact()]
    public async void SetUserRoleTest_EmptyUser_ThrowsException()
    {
        await Assert.ThrowsAsync<Exception>(
            () => this.UserService.SetUserRole(
                userId: null!, 
                role: It.IsAny<RoleEnum>()));
        return;
    }

    [Fact()]
    public async void CreateUserTest_EmptyUser_ThrowsArgumentBullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            () => this.UserService.CreateUser(
                email: null!, 
                password: null!, 
                role: It.IsAny<RoleEnum>()));
        return;
    }

    [Fact()]
    public async void GetUserTest()
    {
        User? result = await this.UserService.GetUser(
            userId: this.TestUser.Id);
        Assert.NotNull(result);
        return;
    }
}
