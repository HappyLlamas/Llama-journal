using Xunit;
using BusinnesLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace BusinnesLayer.Services.Tests;


public class LoginServiceTests
{
    private LoginService LoginService;
    private Mock<IUserRepository> UserRepository;
    private Mock<IOrganizationRepository> OrganizationRepository;

    private User TestUser = new User
    {
        Id = "AA000000",
        FullName = "Test Name",
        Email = "testmail@gmail.com",
        Password = "",
    };

    public LoginServiceTests()
    {
        this.UserRepository = new Mock<IUserRepository>();
        this.SetupUserRepository();

        this.OrganizationRepository = new Mock<IOrganizationRepository>();
        this.SetupOrganizationRepository();

        this.LoginService = new LoginService(
            userRepository: this.UserRepository.Object,
            organizationRepository: this.OrganizationRepository.Object);
        return;
    }

    private void SetupUserRepository()
    {
        this.UserRepository.CallBase = true;
        this.UserRepository
            .Setup(x => x.GetById(this.TestUser.Id))
            .Returns(Task.FromResult<User?>(this.TestUser));
        this.UserRepository
            .Setup(x => x.GetById(""))
            .Returns(Task.FromResult<User?>(null));
        this.UserRepository
            .Setup(x => x.FindByEmail(this.TestUser.Email))
            .Returns(Task.FromResult<User?>(this.TestUser));
        this.UserRepository
            .Setup(x => x.FindByEmail(""))
            .Returns(Task.FromResult<User?>(null));
        // this.UserRepository
        //     .Setup(x => x.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RoleEnum>()))
        //     .Callback((string email, string fullName, RoleEnum role) =>
        //     {
        //         if (email == null || fullName == null)
        //         {
        //             throw new ArgumentNullException();
        //         }
        //     });

        return;
    }

    private void SetupOrganizationRepository()
    {
        this.OrganizationRepository.CallBase = true;
        return;
    }

    [Fact()]
    public void CompleteRegistrationTest_MismatchPasswords_ThrowsInvalidDataException()
    {
        Assert.ThrowsAsync<InvalidDataException>(
            () => this.LoginService.CompleteRegistration(
                userId: this.TestUser.Id,
                password: "1",
                confirmPassword: "2"));
        return;
    }

    [Fact()]
    public void AdminCompleteRegistrationTest_NullUser_ThrowsInvalidDataException()
    {
        Assert.ThrowsAsync<InvalidDataException>(
            () => this.LoginService.AdminCompleteRegistration(
                userId: "",
                fullName: It.IsAny<string>(),
                organizationName: It.IsAny<string>()));
        return;
    }

    [Fact()]
    public void SignUpTest_MismatchPasswords_ThrowsInvalidDataException()
    {
        Assert.ThrowsAsync<InvalidDataException>(
            () => this.LoginService.SignUp(
                email: this.TestUser.Email,
                password: "1",
                confirmPassword: "2"));
        return;
    }

    [Fact()]
    public async void LoginTest_TestUser_NotNull()
    {
        ClaimsIdentity result = await this.LoginService.Login(
            this.TestUser.Email,
            this.TestUser.Password);
        Assert.NotNull(result);
        return;
    }

    [Fact()]
    public void ResetPasswordTest()
    {
        Assert.ThrowsAsync<InvalidDataException>(
            () => this.LoginService.ResetPassword(
                email: "",
                password: It.IsAny<string>(),
                restore_token: It.IsAny<string>()));
        return;
    }
}
