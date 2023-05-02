namespace LlamaJournal.Models;

public class UserCard{
    public string Action { get; set; }
    public string Member { get; set; }
        
    public UserCard(string member, string action)
    {
        Action = action;
        Member = member;
    }
}