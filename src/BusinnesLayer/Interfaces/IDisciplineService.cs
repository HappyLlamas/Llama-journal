using DataLayer.Models;

namespace BusinnesLayer.Services;

public interface IDisciplineService
{
    Task<List<Discipline>> GetAllDisciplines();
    Task AddGroupToDiscipline(int disciplineId, int groupId);
    Task ChangeDisciplineDescription(int disciplineId, string description);
}
