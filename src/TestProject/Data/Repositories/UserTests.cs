using Xunit;
using llama_journal.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using llama_journal.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace llama_journal.Data.Repositories.Tests;


public class UserTests
{
    private JsonElement Connection;
    private string ConnectionString;
    private UserRepository UserRepository;

    private User TestUser = new User
    {
        Id = "AA000000",
        FullName = "Test Name",
        Email = "testmail@gmail.com",
    };

    private User DbUser = new User
    {
        Id = "BB00000000",
        FullName = "Name Full",
        Email = "dbuser@gmail.com",
    };

    public UserTests()
    {
        this.Connection = JsonSerializer
        .Deserialize<JsonElement>(
            json: File.ReadAllText(
                path: "D:\\навчання\\preng\\PerformanceTracker\\PerformanceTracker\\TestProject\\appsettings.Development.json"))!;
        this.ConnectionString = this.Connection
            .GetProperty(propertyName: "ConnectionStrings")
            .GetProperty(propertyName: "Connection")
            .ToString();
        DbContextOptionsBuilder<ModelsContext> db
            = new DbContextOptionsBuilder<ModelsContext>()
            .UseNpgsql(this.ConnectionString);
        this.UserRepository = new UserRepository(
           context: new ModelsContext(options: db.Options));
        return;
    }

    [Fact()]
    public void CreateUserTest()
    {
        this.UserRepository.CreateUser(
            id: this.TestUser.Id,
            email: this.TestUser.Email,
            fullname: this.TestUser.FullName);
        User found = this.UserRepository.FindByEmail(
            email: this.TestUser.Email);
        Assert.NotNull(found);
        return;
    }

    [Fact()]
    public void SetUserPasswordTest()
    {
        string testPassword = "qwerty123";
        User found = this.UserRepository.FindByEmail(
            email: this.TestUser.Email);
        this.UserRepository.SetUserPassword(
            user: found,
            password: testPassword);
        string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: testPassword,
                salt: Convert.FromBase64String(found.PasswordSalt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        Assert.Equal(hashedPassword, found.Password);
        return;
    }

    [Fact()]
    public void CheckUserPasswordTest()
    {
        string testPassword = "qwerty123";
        User found = this.UserRepository.FindByEmail(
            email: this.TestUser.Email);
        this.UserRepository.SetUserPassword(
            user: found,
            password: testPassword);
        string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: testPassword,
                salt: Convert.FromBase64String(found.PasswordSalt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        Assert.True(UserRepository.CheckPassword(
            user: found,
            password: hashedPassword));
        return;
    }

    [Fact()]
    public void FindByEmailTest()
    {
        User found = this.UserRepository.FindByEmail(
            email: this.DbUser.Email);
        Assert.NotNull(found);
        return;
    }

    [Fact()]
    public void GetByIdTest()
    {
        User found = this.UserRepository.GetById(
            userId: this.DbUser.Id);
        Assert.NotNull(found);
        return;
    }

    [Fact()]
    public void SetGroupTest()
    {
        string testGroup = "Admins";
        this.UserRepository.SetGroup(
            user: this.DbUser,
            groupName: testGroup);
        Assert.Equal(DbUser.Group.Name, testGroup);
        return;
    }

    [Fact()]
    public void SetRoleTest()
    {
        RoleEnum testRole = RoleEnum.Admin;
        this.UserRepository.SetRole(
            user: this.DbUser, 
            role: testRole);
        Assert.Equal(DbUser.Role, testRole);
        return;
    }
}
