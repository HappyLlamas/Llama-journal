using Xunit;
using BusinnesLayer.Services;
using Microsoft.Extensions.Logging;
using BusinnesLayer.Models;

namespace BusinnesLayer.Services.Tests;


public class GradesServiceTests
{
    private GradesService GradesService;

    private Mock<IGradeRepository> GradeRepository;
    private Mock<IUserRepository> UserRepository;
    private Mock<IGroupRepository> GroupRepository;
    private Mock<IDisciplineRepository> DisciplineRepository;

    private User TestUser = new User
    {
        Id = "AA000000",
        FullName = "Test Name",
        Email = "testmail@gmail.com",
    };

    public GradesServiceTests()
    {
        this.GradeRepository = new Mock<IGradeRepository>();
        this.SetupGradeRepository();

        this.UserRepository = new Mock<IUserRepository>();
        this.SetupUserRepository();

        this.GroupRepository = new Mock<IGroupRepository>();
        this.SetupGroupRepository();

        this.DisciplineRepository = new Mock<IDisciplineRepository>();
        this.SetupDisciplineRepository();

        this.GradesService = new GradesService(
            gradeRepository: this.GradeRepository.Object,
            userRepository: this.UserRepository.Object,
            disciplineRipository: this.DisciplineRepository.Object,
            groupRepository: this.GroupRepository.Object,
            logger: new Logger<GradesService>(new LoggerFactory()));
        return;
    }

    private void SetupGradeRepository()
    {
        this.GradeRepository.CallBase = true;

        this.GradeRepository
            .Setup(x => x.GetGradesForUserInPeriod(
                It.IsAny<Discipline>(), It.IsAny<User>(),
                It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(Task.FromResult<List<Grade>>(new List<Grade>()));
        this.GradeRepository
            .Setup(x => x.GetGradesForUser(
                It.IsAny<Discipline>(), It.IsAny<User>()))
            .Returns(Task.FromResult<List<Grade>>(new List<Grade>()));
        this.GradeRepository
            .Setup(x => x.GetGradesForGroup(
                It.IsAny<Discipline>(), It.IsAny<Group>()))
            .Returns(Task.FromResult<List<Grade>>(new List<Grade>()));
        this.GradeRepository
            .Setup(x => x.GetById(It.IsAny<long>()))
            .Returns(Task.FromResult<Grade?>(new Grade()));
        this.GradeRepository
            .Setup(x => x.GetById(0))
            .Returns(Task.FromResult<Grade?>(null!));

        this.GradeRepository
            .Setup(x => x.AddGrade(
                It.IsAny<User>(), It.IsAny<int>(), It.IsAny<DateTime>()))
            .Callback((User user, int score, DateTime date) =>
            {
                if (user == null || score == null || date == null)
                {
                    throw new ArgumentNullException();
                }
            });

        return;
    }

    private void SetupUserRepository()
    {
        this.UserRepository.CallBase = true;

        this.UserRepository
            .Setup(x => x.GetById(this.TestUser.Id))
            .Returns(Task.FromResult<User?>(this.TestUser));

        return;
    }

    private void SetupGroupRepository()
    {
        this.GroupRepository.CallBase = true;

        this.GroupRepository
            .Setup(x => x.GetById(It.IsAny<long>()))
            .Returns(Task.FromResult<Group?>(new Group()));

        return;
    }

    private void SetupDisciplineRepository()
    {
        this.DisciplineRepository.CallBase = true;

        this.DisciplineRepository
            .Setup(x => x.GetById(It.IsAny<int>()))
            .Returns(Task.FromResult<Discipline?>(new Discipline()));
        this.DisciplineRepository
            .Setup(x => x.GetGradesForUserInPeriod(
                It.IsAny<Discipline>(), It.IsAny<User>(),
                It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(Task.FromResult<List<Grade>>(new List<Grade>()));

        return;
    }

    [Fact()]
    public void GetGradesForUserInPeriodTest_TestUser_ReturnEmptyGrades()
    {
        Assert.NotNull(this.GradesService.GetGradesForUser(
            userId: this.TestUser.Id,
            disciplineId: 0));
        return;
    }

    [Fact()]
    public async void GetGradesForUserTest_EmptyUser_ThrowsException()
    {
        await Assert.ThrowsAsync<Exception>(
            () => this.GradesService.GetGradesForUser(
                userId: null!, disciplineId: 0));
        return;
    }

    [Fact()]
    public async void GetFileWithGradesTest_TestUser_ReturnsEmptyString()
    {
        string expectedString = "";
        string actualString = await this.GradesService.GetFileWithGrades(
            userId: this.TestUser.Id,
            disciplineId: 0,
            start_datetime: It.IsAny<DateTime>(),
            end_datetime: It.IsAny<DateTime>());
        Assert.Equal(expectedString, actualString);
        return;
    }

    [Fact()]
    public async void AddGradeTest_EmptyUser_ThrowsArgumentNullException()
    {
        await Assert.ThrowsAsync<Exception>(
            () => this.GradesService.AddGrade(null!, 0, It.IsAny<DateTime>()));
        return;
    }

    [Fact()]
    public async void EditGradeCommentTest_EmptyUser_ThrowsException()
    {
        await Assert.ThrowsAsync<Exception>(
            () => this.GradesService.EditGrade(0, 0, null!));
        return;
    }

    [Fact()]
    public async void GetGradesForGroupTest_TestGroup_ReturnsEmptyList()
    {
        List<Grade> result = await this.GradesService.GetGradesForGroup(
            disciplineId: 0,
            groupId: 0,
            start_datetime: It.IsAny<DateTime>(),
            end_datetime: It.IsAny<DateTime>());
        Assert.NotNull(result);
        return;
    }

    [Fact()]
    public async void GetGradeTest_AnyGrade_NotNull()
    {
        Grade? result = await this.GradesService.GetGrade(1);
        Assert.NotNull(result);
        return;
    }

    [Fact()]
    public async void GetGradesDetailTest_TestUser_NotNull()
    {
        GradesDetailModel? result = await this.GradesService.GetGradesDetail(
            userId: this.TestUser.Id, 
            disciplineId: It.IsAny<int>());
        Assert.NotNull(result);
        return;
    }
}
