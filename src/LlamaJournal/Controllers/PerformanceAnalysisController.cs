using BusinnesLayer.Services;
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
        var cards = GetListGroup();
        
        return View(cards);;
    }

    public async Task<List<Card>> GetListGroup()
    {
        var grades = await _gradeService.GetGradesForUser(User.Identity.Name);
        var cards = new List<Card>();
        foreach(var grade in grades) {
            cards.Add(new Card(
                grade.disciplineName,
                grade.teacherName,
                grade.totalGrades
            ));
        }

        return cards;
    }

    public AnalysisModel GetAnalysis()
    {
        var analysis = new AnalysisModel();

        return analysis;
    }
}

public class AnalysisModel
{
    public double AverageScore { get; set; }
    public int PassingGrades { get; set; } //бали > 50
    public int FailingGrades { get; set; }
    public double PassingGradePercentage { get; set; }
    public double FailingGradePercentage { get; set; }
}