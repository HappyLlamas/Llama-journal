using BusinnesLayer.Services;
using DataLayer.Models;
using llama_journal.Models;
using Microsoft.AspNetCore.Mvc;

namespace llama_journal.Controllers;

public class PerformanceAnalysisController: Controller
{
    private readonly IGradeService _gradeService;

    public PerformanceAnalysisController(IGradeService gradeService)
    {
        _gradeService = gradeService;
    }

    public IActionResult Index()
    {
        var users = GetListGroup();
        
        return this.View(users);
    }

    public List<User> GetListGroup()
    {
        List<User> users = new List<User>
        {
            new User(),
            new User(),
            new User(),
            new User(),
        };

        return users;
    }

    public AnalysisModel GetAnalysis()
    {
        var analysis = new AnalysisModel(57,5,1);

        return analysis;
    }
}

public class AnalysisModel
{
    public double AverageScore { get; set; } // середній бал студента зі всіх предметів
    public int PassingGrades { get; set; } //бали > 50
    public int FailingGrades { get; set; }
    public double PassingGradePercentage { get; set; }

    public double FailingGradePercentage { get; set; }

    public AnalysisModel(double averageScore, int passingGrades, int failingGrades)
    {
        AverageScore = averageScore;
        PassingGrades = passingGrades;
        FailingGrades = failingGrades;
        PassingGradePercentage = (passingGrades + failingGrades)/passingGrades;
        FailingGradePercentage = (passingGrades + failingGrades)/failingGrades;
    }
}