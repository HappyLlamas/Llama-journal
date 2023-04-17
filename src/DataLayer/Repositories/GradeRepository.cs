using DataLayer.Models;
using System.Data.Entity;

namespace DataLayer.Repositories
{
public class GradeRepository : IGradeRepository
{
    private readonly ModelsContext _context;
    private readonly IUserRepository _userRepository;

    public GradeRepository(ModelsContext context, IUserRepository userRepository)
    {
        _context = context;
        _userRepository = userRepository;
    }

    public async Task<Grade?> GetById(long id)
    {
        return await _context.Grades.FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<List<Grade>> GetGradesForUser(Discipline discipline, User user)
    {
        return await _context.Grades.Where(g => g.Discipline == discipline && g.User == user).ToListAsync();
    }

    public async Task<List<Grade>> GetGradesForUserInPeriod(Discipline discipline, User user, DateTime startDatetime, DateTime endDatetime)
    {
        return await _context.Grades.Where(g => g.Discipline == discipline && g.User == user && g.Date >= startDatetime && g.Date <= endDatetime).ToListAsync();
    }

    public async Task<List<Grade>> GetGradesForGroup(Discipline discipline, Group group)
    {
        return await _context.Grades.Where(g => g.Discipline == discipline && g.User.Group == group).ToListAsync();
    }

    public async Task AddGrade(User user, int score, DateTime date)
    {
        var grade = new Grade { User = user, Score = score, Date = date };
        _context.Grades.Add(grade);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Grade grade)
    {
        await _context.SaveChangesAsync();
    }
}
}
