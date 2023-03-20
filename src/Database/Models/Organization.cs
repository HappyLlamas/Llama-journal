using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Database;

[Index(nameof(Name), IsUnique=true)]
public class Organization
{
	[Key, MaxLength(12)]
	public long Id { get; set; }

	[MaxLength(250)]
	public string Name { get; set; } = null!;
}
