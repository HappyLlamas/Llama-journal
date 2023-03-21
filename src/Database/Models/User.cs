using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Database;

public enum RoleEnum {
	User = 0,
	Teacher = 1,
	Admin = 2,
}

[Index(nameof(Email), IsUnique = true)]
public class User
{
	[Key, MaxLength(50)]
	public string Id { get; set; } = null!;

	[MaxLength(250), Required]
	public string Email { get; set; } = null!;

	[MaxLength(250)]
	public string FullName { get; set; } = "";

	[Required]
	public RoleEnum Role { get; set; } = RoleEnum.User;

	[Required]
	public Group Group { get; set; } = null!;


	public ICollection<Discipline> TeacherDisciplines { get; set; } = new List<Discipline>();
}
