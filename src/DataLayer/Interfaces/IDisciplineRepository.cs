using DataLayer.Models;

namespace DataLayer.Repositories
{
public interface IDisciplineRepository
{
    List<Discipline> GetDisciplines();
    Discipline? GetById(int id);
    List<Discipline> GetAll();
    void AddGroupToDiscipline(Discipline discipline, Group group);
    void Update(Discipline discipline);
    List<Grade> GetGradesForUserInPeriod(Discipline discipline, string userId, DateTime startDate, DateTime endDate);
}
}
