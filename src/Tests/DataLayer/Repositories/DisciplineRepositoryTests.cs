// using DataLayer.Repositories;
// using DataLayer.Models;
// using Tests.Fixtures;
//
// namespace Tests.DataLayer.Repositories;
//
//
// public class DisciplineRepositoryTests
// {
//     private DisciplineRepository DisciplineRepository;
//
//     private Discipline TestDiscipline = new Discipline
//     {
//         Name = "Test Discipline",
//     };
//
//     private Discipline AnotherDiscipline = new Discipline
//     {
//         Name = "Another Discipline",
//     };
//
//     public DisciplineRepositoryTests()
//     {
// 		var db = TestDbContext.Get();
//         this.DisciplineRepository = new DisciplineRepository(db);
// 		db.Disciplines.Add(TestDiscipline);
// 		db.SaveChanges();
//     }
//
//     [Fact()]
//     public void GetDisciplinesTest()
//     {
//         List<Discipline> testGroups
//             = this.DisciplineRepository.GetDisciplines();
//         Assert.NotNull(testGroups);
//         return;
//     }
//
//     [Fact()]
//     public void GetAllTest()
//     {
//         List<Discipline> testGroups
//             = this.DisciplineRepository.GetAll();
//         Assert.NotNull(testGroups);
//         return;
//     }
//
//     [Fact()]
//     public void GetByIdTest()
//     {
//         Discipline testDiscipline = this.DisciplineRepository.GetById((int)TestDiscipline.Id);
//         Assert.Equal(testDiscipline.Name, this.TestDiscipline.Name);
//         return;
//     }
//
//     [Fact()]
//     public void GetGradesForUserInPeriodTest()
//     {
//         List<Grade> testGrades
//             = this.DisciplineRepository.GetGradesForUserInPeriod(
//                 discipline: this.TestDiscipline,
//                 userId: "BB00000000",
//                 startDate: DateTime.MinValue,
//                 endDate: DateTime.Today);
//         Assert.NotNull(testGrades);
//         return;
//     }
//
//     [Fact()]
//     public void AddGroupToDisciplineTest()
//     {
//         Group testGroup = new Group { Id = 0, Name = "Admins" };
//         this.DisciplineRepository.AddGroupToDiscipline(
//             discipline: this.TestDiscipline,
//             group: testGroup);
//         Assert.NotEmpty(this.TestDiscipline.Groups);
//         return;
//     }
//
//     [Fact()]
//     public void UpdateTest()
//     {
//         Discipline testDiscipline = this.DisciplineRepository.GetById(id: (int)TestDiscipline.Id);
//         testDiscipline.Name = "Changed";
//         this.DisciplineRepository.UpdateUser(testDiscipline);
//         List<Discipline> testGroups = this.DisciplineRepository.GetAll();
//         Assert.Null(testGroups.Find(g => g.Name == this.TestDiscipline.Name));
//         testDiscipline.Name = "Admins";
//         this.DisciplineRepository.UpdateUser(TestDiscipline);
//         return;
//     }
// }
