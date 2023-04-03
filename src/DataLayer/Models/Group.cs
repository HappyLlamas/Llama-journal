using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Models;

[Index(nameof(Name), IsUnique = true)]
public class Group
{
    [Key, MaxLength(12)]
    public long Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public Organization Organization { get; set; } = null!;

    public ICollection<Discipline> Disciplines { get; set; } = new List<Discipline>();
}
