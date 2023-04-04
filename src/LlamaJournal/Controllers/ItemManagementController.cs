using Microsoft.AspNetCore.Mvc;

namespace llama_journal.Controllers;


public class ItemManagementController:Controller
{
    private const int PageSize = 4;
    public IList<InfoItemCard> InfoCards = new List<InfoItemCard>
    {
        new InfoItemCard( "lector_1", new List<int> { 4, 3, 3, 5 }, "math"),
        new InfoItemCard ("lector_2", new List<int> { 4, 3, 3, 5 }, "math"),
        new InfoItemCard ("lector_3", new List<int> { 4, 3, 3, 5 }, "math"),
    };
    public IActionResult Index(int pageIndex = 1)
    {
        // витягти усі дисципліни з юзера, і замити ним клас Cards
        var infoCards = InfoCards.OrderBy(c => c.Subject)
            .Skip((pageIndex - 1) * PageSize)
            .Take(PageSize)
            .ToList();

        return this.View(infoCards);
    }
}

public class InfoItemCard
{
    public string Subject { get; set; }

    public string FullName { get; set; }

    public List<int> Grades { get; set; }

    public InfoItemCard(string fullName, List<int> grades, string subject)
    {
        FullName = fullName;
        Grades = grades;
        Subject = subject;
    }
}