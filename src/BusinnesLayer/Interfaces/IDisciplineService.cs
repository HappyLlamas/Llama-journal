using DataLayer.Models;

namespace BusinnesLayer.Services;

public interface IDisciplineService
{
    Task<List<Discipline>> GetAllDisciplines(string userId);
    Task AddGroupToDiscipline(int disciplineId, int groupId);
    Task ChangeDisciplineDescription(int disciplineId, string description);
}
