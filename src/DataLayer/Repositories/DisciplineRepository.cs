using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
public class DisciplineRepository : IDisciplineRepository
{
    private readonly ModelsContext _context;

    public DisciplineRepository(ModelsContext context)
    {
        _context = context;
    }

    public async Task<List<Discipline>> GetDisciplines(User? user=null)
    {
		if(user != null)
			return await _context.Disciplines.Where(discipline => discipline.Groups.Contains(user.Group)).ToListAsync();
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

    public async Task<List<Grade>> GetGradesForUserInPeriod(Discipline discipline, User user, DateTime? startDate=null, DateTime? endDate=null)
    {
		var query = _context.Grades.Where(g => g.Discipline == discipline && g.User.Id == user.Id);

		if(startDate != null)
			query = query.Where(g => g.Date >= startDate);

		if(endDate != null)
			query = query.Where(g => g.Date <= endDate);

		return await query.ToListAsync();
    }

}
}
