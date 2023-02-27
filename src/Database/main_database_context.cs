using Microsoft.EntityFrameworkCore;

namespace Database;

public class DatabaseContext : DbContext
{
	// Add your models here
	// Format: public DbSet<Model> ModelName { get; set; }

	public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options){}
}
