using System.ComponentModel.DataAnnotations;
using DataLayer.Models;

namespace LlamaJournal.Models;

public class CreateUser
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

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}