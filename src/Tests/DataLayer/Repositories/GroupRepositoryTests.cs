// using Xunit;
// using DataLayer.Repositories;
// using DataLayer.Models;
// using Tests.Fixtures;
//
// namespace Tests.DataLayer.Repositories;
//
//
// public class GroupRepositoryTests
// {
//     private GroupRepository GroupRepository;
//
//     private Group TestGroup = new Group
//     {
//         Name = "Test Group",
//     };
//
//     private Group AnotherGroup = new Group
//     {
//         Name = "Another Group",
//     };
//
//     private Group DbGroup = new Group
//     {
// 		Id = 0,
//         Name = "Admins",
//     };
//
//     public GroupRepositoryTests()
//     {
// 		var db = TestDbContext.Get();
//         this.GroupRepository = new GroupRepository(db);
// 		db.Groups.Add(DbGroup);
// 		db.SaveChanges();
//     }
//
//     [Fact()]
//     public void GetGroupsTest()
//     {
//         List<Group> testGroups = this.GroupRepository.GetGroups();
//         Assert.NotNull(testGroups);
//         return;
//     }
//
//     [Fact()]
//     public void GetByIdTest()
//     {
//         string groupName = "Admins";
//         Group testGroup = this.GroupRepository.GetById(id: 0);
//         Assert.Equal(testGroup.Name, groupName);
//         return;
//     }
//
//     [Fact()]
//     public void AddTest()
//     {
//         this.GroupRepository.Add(group: this.TestGroup);
//         List<Group> testGroups = this.GroupRepository.GetGroups();
//         Assert.NotNull(testGroups.Find(g => g.Name == this.TestGroup.Name));
//         this.GroupRepository.Delete(group: this.TestGroup);
//         return;
//     }
//
//     [Fact()]
//     public void UpdateTest()
//     {
//         Group testGroup = this.GroupRepository.GetById(id: 0);
//         testGroup.Name = "Changed";
//         this.GroupRepository.UpdateUser(testGroup);
//         List<Group> testGroups = this.GroupRepository.GetGroups();
//         Assert.Null(testGroups.Find(g => g.Name == this.TestGroup.Name));
//         testGroup.Name = "Admins";
//         this.GroupRepository.UpdateUser(TestGroup);
//         return;
//     }
//
//     [Fact()]
//     public void DeleteTest()
//     {
//         List<Group> testGroups = this.GroupRepository.GetGroups();
//         this.GroupRepository.Add(group: this.TestGroup);
//         this.GroupRepository.Delete(group: this.TestGroup);
//         Assert.Null(testGroups.Find(g => g.Name == this.TestGroup.Name));
//         return;
//     }
// }
