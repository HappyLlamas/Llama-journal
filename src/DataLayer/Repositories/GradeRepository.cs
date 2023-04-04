using DataLayer.Models;

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

    public Grade GetById(long id)
    {
        var result = _context.Set<Grade>().SingleOrDefault(g => g.Id == id);
		if(result == null)
			throw new Exception($"Grade with id {id} not found");
		return result;
    }

    public List<Grade> GetGradesForUser(Discipline discipline, User user)
    {
        return _context.Set<Grade>().Where(g => g.Discipline == discipline && g.User == user).ToList();
    }

    public List<Grade> GetGradesForUserInPeriod(Discipline discipline, User user, DateTime startDatetime, DateTime endDatetime)
    {
        return _context.Set<Grade>().Where(g => g.Discipline == discipline && g.User == user && g.Date >= startDatetime && g.Date <= endDatetime).ToList();
    }

    public List<Grade> GetGradesForGroup(Discipline discipline, Group group)
    {
        return _context.Set<Grade>().Where(g => g.Discipline == discipline && g.User.Group == group).ToList();
    }

    public void AddGrade(string userId, int score, DateTime date)
    {
        var grade = new Grade { User = _userRepository.GetById(userId), Score = score, Date = date };
        _context.Set<Grade>().Add(grade);
        _context.SaveChanges();
    }

    public void SetGradeComment(Grade grade, string comment)
    {
        grade.Comment = comment;
        _context.SaveChanges();
    }
}
}
