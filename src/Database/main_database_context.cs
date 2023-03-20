using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace llama_journal.Database;
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed.")]
public class DatabaseContext : DbContext
{
    // Add your models here
    // Format: public DbSet<Model> ModelName { get; set; }
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }
}
