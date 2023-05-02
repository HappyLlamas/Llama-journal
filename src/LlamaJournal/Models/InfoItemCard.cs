namespace llama_journal.Controllers;

public class InfoItemCard
{
    public long DisciplineId { get; set; }

    public string StudentId { get; set; }
    public string Name { get; set; }
    public string Subject { get; set; }

    public string Group { get; set; }
    

    public InfoItemCard(long disciplineId, string studentId, string name, string subject, string group)
    {
        DisciplineId = disciplineId;
        Subject = subject;
        Group = group;
        StudentId = studentId;
        Name = name;
    }
}