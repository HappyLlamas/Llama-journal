using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace llama_journal.Controllers;

public class Card
{
    public string Subject { get; set; }
    public string FullName { get; set; }
    public string GradesUrl { get; set; }

    public Card(string subject, string fullName, string gradesUrl)
    {
        Subject = subject;
        FullName = fullName;
        GradesUrl = gradesUrl;
    }
}
public class ProgressController: Controller
{
    private const int PageSize = 2;
    public IList<Card> Cards = new List<Card>
    {
        new Card("math", "Julian", "refer_link"),
        new Card("math", "Julian", "refer_link"),
        new Card("math", "Julian", "refer_link"),
        new Card("math", "Julian", "refer_link"),
        new Card("math", "Julian", "refer_link"),
        new Card("math", "Julian", "refer_link"),
    };
   
    public IActionResult Index(int pageIndex = 1)
    {
        var cards = Cards.OrderBy(c => c.Subject)
            .Skip((pageIndex - 1) * PageSize)
            .Take(PageSize)
            .ToList();

        return this.View(cards);
    }
}