namespace BusinnesLayer.Models;

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

    public AnalysisModel()
    {
        
    }
}