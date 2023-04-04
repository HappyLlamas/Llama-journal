using DataLayer.Models;

namespace DataLayer.Repositories
{
public class DisciplineRepository : IDisciplineRepository
{
    private readonly ModelsContext _context;

    public DisciplineRepository(ModelsContext context)
    {
        _context = context;
    }

    public List<Discipline> GetDisciplines()
    {
        return _context.Disciplines.ToList();
    }

    public Discipline GetById(int id)
    {
        var result = _context.Disciplines.Find(id);
		if(result == null)
			throw new Exception($"Discipline with id {id} not found");
		return result;
    }

    public List<Discipline> GetAll()
    {
        return _context.Disciplines.ToList();
    }


    public void AddGroupToDiscipline(Discipline discipline, Group group)
    {
        discipline.Groups.Add(group);
        _context.SaveChanges();
    }

    public void Update(Discipline discipline)
    {
        _context.SaveChanges();
    }

    public List<Grade> GetGradesForUserInPeriod(Discipline discipline, string userId, DateTime startDate, DateTime endDate)
    {
        return _context.Grades
               .Where(g => g.Discipline == discipline && g.User.Id == userId && g.Date >= startDate && g.Date <= endDate)
               .ToList();
    }

}
}
