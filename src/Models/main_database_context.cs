using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace llama_journal.Models;

[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed.")]
public class ModelsContext : DbContext
{
	// Add your models here
	// Format: public DbSet<Model> ModelName { get; set; }

	public DbSet<Organization> Organizations { get; set; }
	public DbSet<Group> Groups { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<Discipline> Disciplines { get; set; }
	public DbSet<Attendance> Attendances { get; set; }
	public DbSet<Grade> Grades { get; set; }

	public ModelsContext(DbContextOptions<ModelsContext> options) : base(options){}
}
