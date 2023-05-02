using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Models;

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

    public Group? Group { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

	[Required]
	public bool CompleteRegistration { get; set; } = false;

    public ICollection<Discipline> TeacherDisciplines { get; set; } = new List<Discipline>();
    public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    
}
