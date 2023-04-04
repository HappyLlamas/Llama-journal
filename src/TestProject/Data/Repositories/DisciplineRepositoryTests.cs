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


public class DisciplineRepositoryTests
{
    private JsonElement Connection;
    private string ConnectionString;
    private DisciplineRepository DisciplineRepository;

    private Discipline TestDiscipline = new Discipline
    {
        Name = "Test Discipline",
    };

    private Discipline AnotherDiscipline = new Discipline
    {
        Name = "Another Discipline",
    };

    public DisciplineRepositoryTests()
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
        this.DisciplineRepository = new DisciplineRepository(
           context: new ModelsContext(options: db.Options));
        return;
    }

    [Fact()]
    public void GetDisciplinesTest()
    {
        List<Discipline> testGroups
            = this.DisciplineRepository.GetDisciplines();
        Assert.NotNull(testGroups);
        return;
    }

    [Fact()]
    public void GetAllTest()
    {
        List<Discipline> testGroups
            = this.DisciplineRepository.GetAll();
        Assert.NotNull(testGroups);
        return;
    }

    [Fact()]
    public void GetByIdTest()
    {
        Discipline testDiscipline = this.DisciplineRepository.GetById(id: 0);
        Assert.Equal(testDiscipline.Name, this.TestDiscipline.Name);
        return;
    }

    [Fact()]
    public void GetGradesForUserInPeriodTest()
    {
        List<Grade> testGrades
            = this.DisciplineRepository.GetGradesForUserInPeriod(
                discipline: this.TestDiscipline,
                userId: "BB00000000",
                startDate: DateTime.MinValue,
                endDate: DateTime.Today);
        Assert.NotNull(testGrades);
        return;
    }

    [Fact()]
    public void AddGroupToDisciplineTest()
    {
        Group testGroup = new Group { Id = 0, Name = "Admins" };
        this.DisciplineRepository.AddGroupToDiscipline(
            discipline: this.TestDiscipline,
            group: testGroup);
        Assert.NotEmpty(this.TestDiscipline.Groups);
        return;
    }

    [Fact()]
    public void UpdateTest()
    {
        Discipline testDiscipline = this.DisciplineRepository.GetById(id: 0);
        testDiscipline.Name = "Changed";
        this.DisciplineRepository.Update(testDiscipline);
        List<Discipline> testGroups = this.DisciplineRepository.GetAll();
        Assert.Null(testGroups.Find(g => g.Name == this.TestDiscipline.Name));
        testDiscipline.Name = "Admins";
        this.DisciplineRepository.Update(TestDiscipline);
        return;
    }
}
