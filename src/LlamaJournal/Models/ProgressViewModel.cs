using DataLayer.Models;


namespace llama_journal.Models;


public class ProgressViewModel
{
	public User User { get; set; }
	public List<Grade> Grades { get; set; }
	public double AverageScore { get; set; }
	public int MaxScore { get; set; }
	public int MinScore { get; set; }
	public int NumGrades { get; set; }
	public int PassingGrades { get; set; }
	public int FailingGrades { get; set; }
	public double PassingGradePercentage { get; set; }
	public double FailingGradePercentage { get; set; }
	public string Subject { get; set; }
	public string FullName { get; set; }
}
public class Card
{
	public string Subject { get; set; }
	public string FullName { get; set; }
	public int TotalGrades { get; set; }

	public Card(string subject, string fullName, int totalGrades)
	{
		Subject = subject;
		FullName = fullName;
		TotalGrades = totalGrades;
	}
}
