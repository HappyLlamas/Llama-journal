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
            .Returns(Task.FromResult<Group?>(null!));

        return;
    }

    private void SetupDisciplineRepository()
    {
        this.DisciplineRepository.CallBase = true;

        this.DisciplineRepository
            .Setup(x => x.GetById(It.IsAny<int>()))
            .Returns(Task.FromResult<Discipline?>(new Discipline()));
        this.DisciplineRepository
            .Setup(x => x.GetById(0))
            .Returns(Task.FromResult<Discipline?>(null));
        this.DisciplineRepository
            .Setup(x => x.GetGradesForUserInPeriod(
                It.IsAny<Discipline>(), It.IsAny<User>(),
                DateTime.MinValue, DateTime.MaxValue))
            .Returns(Task.FromResult<List<Grade>>(new List<Grade>()));
        this.DisciplineRepository
            .Setup(x => x.GetDisciplines(It.IsAny<User>()))
            .Returns(Task.FromResult<List<Discipline>>(new List<Discipline>()));

        return;
    }

    [Fact()]
    public void GetAllDisciplinesTest_ReturnsEmptyList()
    {
        Assert.NotNull(this.DisciplineService.GetAllDisciplines(
            userId: It.IsAny<string>()));
        return;
    }

    [Fact()]
    public async void AddGroupToDisciplineTest_EmptyGroup_ThrowsException()
    {
        await Assert.ThrowsAsync<Exception>(
            () => this.DisciplineService.AddGroupToDiscipline(
                disciplineId: 0, groupId: 0));
        return;
    }

    [Fact()]
    public async void ChangeDisciplineDescriptionTest_BlankDescription_ReturnsTrue()
    {
        await Assert.ThrowsAsync<Exception>(
            () => this.DisciplineService.ChangeDisciplineDescription(
                disciplineId: 0, description: ""));
        return;
    }
}
