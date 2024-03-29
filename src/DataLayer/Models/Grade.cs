using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models;

public class Grade
{
    [Key, MaxLength(12)]
    public long Id { get; set; }

    [Required, Range(0, 100)]
    public int Score { get; set; }

    [Column(TypeName="Date"), DataType(DataType.Date)]
    public DateTime Date { get; set; } = DateTime.Now;

    [MaxLength(200)]
    public string? Comment { get; set; }

    [Required]
    public User User { get; set; } = null!;

    [Required]
    public Discipline Discipline { get; set; } = null!;
}
