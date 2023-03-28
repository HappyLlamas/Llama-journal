using System.Collections.Generic;
using llama_journal.Models;

namespace llama_journal.Data.Repositories
{
    public interface IDisciplineRepository
    {
        List<Discipline> GetDisciplines();
        Discipline GetById(int id);
        List<Discipline> GetAll();
        void AddGroupToDiscipline(Discipline discipline, Group group);
        void Update(Discipline discipline);
        List<Grade> GetGradesForUserInPeriod(Discipline discipline, string userId, DateTime startDate, DateTime endDate);
    }
}