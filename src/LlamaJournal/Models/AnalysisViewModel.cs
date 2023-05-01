using DataLayer.Models;

namespace llama_journal.Models;

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

public class AnalysisUserModel
{
	public string Id;
	public string FullName;
	public string Group;

	public AnalysisUserModel(User user)
	{
		Id = user.Id;
		FullName = user.FullName;
		Group = user.Group.Name;
	}
}
