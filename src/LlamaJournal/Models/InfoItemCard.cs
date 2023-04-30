namespace llama_journal.Controllers;

public class InfoItemCard
{
    public string Subject { get; set; }

    public string Group { get; set; }
    

    public InfoItemCard(string subject, string group)
    {
        Subject = subject;
        Group = group;
    }
}