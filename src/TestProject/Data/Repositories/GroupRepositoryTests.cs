using Xunit;
using llama_journal.Data.Repositories;
using llama_journal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace llama_journal.Data.Repositories.Tests;


public class GroupRepositoryTests
{
    private JsonElement Connection;
    private string ConnectionString;
    private GroupRepository GroupRepository;

    private Group TestGroup = new Group
    {
        Name = "Test Group",
    };

    private Group AnotherGroup = new Group
    {
        Name = "Another Group",
    };

    public GroupRepositoryTests()
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
        this.GroupRepository = new GroupRepository(
           context: new ModelsContext(options: db.Options));
        return;
    }

    [Fact()]
    public void GetGroupsTest()
    {
        List<Group> testGroups = this.GroupRepository.GetGroups();
        Assert.NotNull(testGroups);
        return;
    }

    [Fact()]
    public void GetByIdTest()
    {
        string groupName = "Admins";
        Group testGroup = this.GroupRepository.GetById(id: 0);
        Assert.Equal(testGroup.Name, groupName);
        return;
    }

    [Fact()]
    public void AddTest()
    {
        this.GroupRepository.Add(group: this.TestGroup);
        List<Group> testGroups = this.GroupRepository.GetGroups();
        Assert.NotNull(testGroups.Find(g => g.Name == this.TestGroup.Name));
        this.GroupRepository.Delete(group: this.TestGroup);
        return;
    }

    [Fact()]
    public void UpdateTest()
    {
        Group testGroup = this.GroupRepository.GetById(id: 0);
        testGroup.Name = "Changed";
        this.GroupRepository.Update(testGroup);
        List<Group> testGroups = this.GroupRepository.GetGroups();
        Assert.Null(testGroups.Find(g => g.Name == this.TestGroup.Name));
        testGroup.Name = "Admins";
        this.GroupRepository.Update(TestGroup);
        return;
    }

    [Fact()]
    public void DeleteTest()
    {
        List<Group> testGroups = this.GroupRepository.GetGroups();
        this.GroupRepository.Add(group: this.TestGroup);
        this.GroupRepository.Delete(group: this.TestGroup);
        Assert.Null(testGroups.Find(g => g.Name == this.TestGroup.Name));
        return;
    }
}