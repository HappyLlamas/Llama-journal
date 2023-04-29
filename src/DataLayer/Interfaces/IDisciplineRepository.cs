using DataLayer.Models;

namespace DataLayer.Repositories
{
public interface IDisciplineRepository
{
    Task<List<Discipline>> GetDisciplines(User? user=null);
    Task<Discipline?> GetById(int id);
    Task<List<Discipline>> GetAll();
    Task Update(Discipline discipline);
    Task<List<Grade>> GetGradesForUserInPeriod(Discipline discipline, User user, DateTime? startDate=null, DateTime? endDate=null);
}
}
