using DataLayer.Models;

namespace llama_journal.Controllers;

public class InfoItemCard
{
    public string Subject { get; set; }

    public User FullNameTeachers { get; set; }

    public List<Grade> Grades { get; set; }

    public InfoItemCard(User fullNameTeachers, List<Grade> grades, string subject)
    {
        FullNameTeachers = fullNameTeachers;
        Grades = grades;
        Subject = subject;
    }
}