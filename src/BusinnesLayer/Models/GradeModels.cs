namespace BusinnesLayer.Models;

public class GradesDetailModel
{
	public double averageScore { get; set; }
	public double maxScore { get; set; }
	public double minScore { get; set; }
	public double numGrades { get; set; }
}
public class GradesPerDiscipline
{
	public string disciplineName { get; set; }
	public string teacherName { get; set; }
	public List<int> grades { get; set; }
}
