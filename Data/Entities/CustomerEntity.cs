using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities;

[Index(nameof(Email), IsUnique = true)]
public class CustomerEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}