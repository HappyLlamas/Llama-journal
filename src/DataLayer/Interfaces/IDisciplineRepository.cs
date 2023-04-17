using DataLayer.Models;

namespace DataLayer.Repositories
{
public interface IDisciplineRepository
{
    Task<List<Discipline>> GetDisciplines();
    Task<Discipline?> GetById(int id);
    Task<List<Discipline>> GetAll();
    Task Update(Discipline discipline);
    Task<List<Grade>> GetGradesForUserInPeriod(Discipline discipline, string userId, DateTime startDate, DateTime endDate);
}
}
