using DataLayer.Models;
using System.Data.Entity;

namespace DataLayer.Repositories
{
public class DisciplineRepository : IDisciplineRepository
{
    private readonly ModelsContext _context;

    public DisciplineRepository(ModelsContext context)
    {
        _context = context;
    }

    public async Task<List<Discipline>> GetDisciplines()
    {
        return await _context.Disciplines.ToListAsync();
    }

    public async Task<Discipline?> GetById(int id)
    {
        return await _context.Disciplines.FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<List<Discipline>> GetAll()
    {
        return await _context.Disciplines.ToListAsync();
    }

    public async Task Update(Discipline discipline)
    {
        await _context.SaveChangesAsync();
    }

    public async Task<List<Grade>> GetGradesForUserInPeriod(Discipline discipline, string userId, DateTime startDate, DateTime endDate)
    {
        return await _context.Grades
               .Where(g => g.Discipline == discipline && g.User.Id == userId && g.Date >= startDate && g.Date <= endDate)
               .ToListAsync();
    }

}
}
