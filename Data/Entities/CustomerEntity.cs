using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities;

[Index(nameof(CustomerEmail), IsUnique = true)]
public class CustomerEntity
{
    [Key]
    public int Id { get; set; }
    public string CustomerName { get; set; } = null!;
    public string CustomerEmail { get; set; } = null!;
    
    // One-to-many relationship
    public ICollection<ProjectEntity> Projects { get; set; }
}