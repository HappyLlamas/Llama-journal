using BusinnesLayer.Services;
using DataLayer.Repositories;


public static class ServicesExtentions
{
    public static void AddBusinessLayerServices(this IServiceCollection services)
    {
        services.AddScoped<IDisciplineService, DisciplineService>();
        services.AddScoped<IGradeService, GradesService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IUserService, UserService>();
    }

    public static void AddDataLayerServices(this IServiceCollection services)
    {
        services.AddScoped<IDisciplineRepository, DisciplineRepository>();
        services.AddScoped<IGradeRepository, GradeRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}