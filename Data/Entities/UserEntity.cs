using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class UserEntity
{
    [Key]
    public int Id { get; set; } 
    public string UserFirstName { get; set; } = null!;
    public string UserLastName { get; set; } = null!;
    public string UserEmail { get; set; } = null!;
    
    public int RoleId { get; set; }
    
    // Many-to-one-relationship
    [ForeignKey("RoleId")]
    public RoleEntity Role { get; set; } = null!;
    
    // One-to-many relationship
    public ICollection<ProjectEntity> Projects { get; set; }
}