using Microsoft.EntityFrameworkCore;

namespace llama_journal.Models;

public class llama_journal.ModelsContext : DbContext
{
	// Add your models here
	// Format: public DbSet<Model> ModelName { get; set; }

	public DbSet<Organization> Organizations { get; set; }
	public DbSet<Group> Groups { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<Discipline> Disciplines { get; set; }
	public DbSet<Attendance> Attendances { get; set; }

	public llama_journal.ModelsContext(DbContextOptions<llama_journal.ModelsContext> options) : base(options){}
}
