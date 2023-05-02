using System.ComponentModel.DataAnnotations;
using DataLayer.Models;

namespace LlamaJournal.Models;

public class DeleteUser
{
    [Key, MaxLength(50)]
    public string Id { get; set; } = null!;
    
}