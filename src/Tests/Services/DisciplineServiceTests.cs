namespace BusinnesLayer.Services.Tests;


public class DisciplineServiceTests
{
    private DisciplineService DisciplineService;

    private Mock<IUserRepository> UserRepository;
    private Mock<IGroupRepository> GroupRepository;
    private Mock<IDisciplineRepository> DisciplineRepository;

    public DisciplineServiceTests()
    {
        this.UserRepository = new Mock<IUserRepository>();
        this.SetupUserRepository();

        this.GroupRepository = new Mock<IGroupRepository>();
        this.SetupGroupRepository();

        this.DisciplineRepository = new Mock<IDisciplineRepository>();
        this.SetupDisciplineRepository();

        this.DisciplineService = new DisciplineService(
            userRepository: this.UserRepository.Object,
            disciplineRepository: this.DisciplineRepository.Object,
            groupRepository: this.GroupRepository.Object);
        return;
    }

    private void SetupUserRepository()
    {
        this.UserRepository.CallBase = true;
        return;
    }

    private void SetupGroupRepository()
    {
        this.GroupRepository.CallBase = true;

        this.GroupRepository
            .Setup(x => x.GetById(It.IsAny<long>()))
            .Returns(null as Group);

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
        this.DisciplineRepository
            .Setup(x => x.GetDisciplines())
            .Returns(new List<Discipline>());

        this.DisciplineRepository
            .Setup(x => x.AddGroupToDiscipline(
                It.IsAny<Discipline>(), It.IsAny<Group>()))
            .Callback((Discipline discipline, Group group) =>
            {
                if (discipline == null || group == null)
                {
                    throw new ArgumentNullException();
                }
            });

        return;
    }

    [Fact()]
    public void GetAllDisciplinesTest_ReturnsEmptyList()
    {
        Assert.NotNull(this.DisciplineService.GetAllDisciplines());
        return;
    }

    [Fact()]
    public void AddGroupToDisciplineTest_EmptyGroup_ThrowsException()
    {
        Assert.Throws<Exception>(
            () => this.DisciplineService.AddGroupToDiscipline(
                disciplineId: 0, groupId: 0));
        return;
    }

    [Fact()]
    public void ChangeDisciplineDescriptionTest_BlankDescription_ReturnsTrue()
    {
        Assert.True(this.DisciplineService.ChangeDisciplineDescription(
            disciplineId: 0, description: ""));
        return;
    }
}
