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
            groupRepository: this.GroupRepository.Object);
        return;
    }

    private void SetupGradeRepository()
    {
        this.GradeRepository.CallBase = true;

        this.GradeRepository
            .Setup(x => x.GetGradesForUserInPeriod(
                It.IsAny<Discipline>(), It.IsAny<User>(),
                DateTime.MinValue, DateTime.MaxValue))
            .Returns(new List<Grade>());
        this.GradeRepository
            .Setup(x => x.GetGradesForUser(
                It.IsAny<Discipline>(), It.IsAny<User>()))
            .Returns(new List<Grade>());
        this.GradeRepository
            .Setup(x => x.GetGradesForGroup(
                It.IsAny<Discipline>(), It.IsAny<Group>()))
            .Returns(new List<Grade>());

        this.GradeRepository
            .Setup(x => x.AddGrade(
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>()))
            .Callback((string userId, int score, DateTime date) =>
            {
                if (userId == null || score == null || date == null)
                {
                    throw new ArgumentNullException();
                }
            });

        this.GradeRepository
            .Setup(x => x.SetGradeComment(
                It.IsAny<Grade>(), It.IsAny<string>()))
            .Callback((Grade grade, string comment) =>
            {
                if (grade == null || comment == null)
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
            .Returns(this.TestUser);

        return;
    }

    private void SetupGroupRepository()
    {
        this.GroupRepository.CallBase = true;

        this.GroupRepository
            .Setup(x => x.GetById(It.IsAny<long>()))
            .Returns(new Group());

        return;
    }

    private void SetupDisciplineRepository()
    {
        this.DisciplineRepository.CallBase = true;

        this.DisciplineRepository
            .Setup(x => x.GetById(It.IsAny<int>()))
            .Returns(new Discipline());
        this.DisciplineRepository
            .Setup(x => x.GetGradesForUserInPeriod(
                It.IsAny<Discipline>(), It.IsAny<string>(),
                DateTime.MinValue, DateTime.MaxValue))
            .Returns(new List<Grade>());

        return;
    }

    [Fact()]
    public void GetGradesForUserInPeriodTest()
    {
        Assert.NotNull(this.GradesService.GetGradesForUser(
            userId: this.TestUser.Id, 
            disciplineId: 0, 
            start_datetime: DateTime.MinValue, 
            end_datetime: DateTime.MaxValue));
        return;
    }

    [Fact()]
    public void GetGradesForUserTest()
    {
        Assert.Throws<Exception>(
            () => this.GradesService.GetGradesForUser(
                userId: null!, disciplineId: 0));
        return;
    }

    [Fact()]
    public void GetAvarangeGradeTest()
    {
        double expectedAverage = 0.0;
        Assert.Equal(
            expected: expectedAverage, 
            actual: this.GradesService.GetAvarangeGrade(
                userId: this.TestUser.Id, disciplineId: 0));
        return;
    }

    [Fact()]
    public void GetFileWithGradesTest()
    {
        string expectedString = "";
        Assert.Equal(
            expected: expectedString,
            actual: this.GradesService.GetFileWithGrades(
                userId: this.TestUser.Id, 
                disciplineId: 0,
                start_datetime: DateTime.Now,
                end_datetime: DateTime.Now));
        return;
    }

    [Fact()]
    public void GetGradesForAllUserDisciplinesTest()
    {
        Assert.NotNull(this.GradesService.GetGradesForAllUserDisciplines(
            userId: this.TestUser.Id,
            startDatetime: DateTime.Now,
            endDatetime: DateTime.Now));
        return;
    }

    [Fact()]
    public void AddGradeTest()
    {
        Assert.Throws<ArgumentNullException>(
            () => this.GradesService.AddGrade(null!, 0, DateTime.Now));
        return;
    }

    [Fact()]
    public void AddGradeCommentTest()
    {
        Assert.Throws<Exception>(
            () => this.GradesService.AddGradeComment(0, null!));
        return;
    }

    [Fact()]
    public void GetGradesForGroupTest()
    {
        Assert.NotNull(this.GradesService.GetGradesForGroup(
            disciplineId: 0,
            groupId: 0,
            start_datetime: DateTime.Now,
            end_datetime: DateTime.Now));
        return;
    }
}
