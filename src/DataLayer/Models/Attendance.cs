using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models;

public class Attendance
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName="Date"), DataType(DataType.Date), Required]
    public DateTime Date { get; set; } = DateTime.Now;

    [Required]
    public User User { get; set; } = null!;

    [Required]
    public Discipline Discipline { get; set; } = null!;
}
