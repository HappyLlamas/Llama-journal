using Microsoft.EntityFrameworkCore;

namespace Database;

public class DatabaseContext : DbContext
{
	// Add your models here
	// Format: public DbSet<Model> ModelName { get; set; }

	public DbSet<Organization> Organizations { get; set; }
	public DbSet<Group> Groups { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<Discipline> Disciplines { get; set; }
	public DbSet<Attendance> Attendances { get; set; }

	public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options){}
}
