using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

[Table("Projects")]
public class ProjectEntity
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    
    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; } 
    
    [Column(TypeName = "date")]
    public DateTime? EndDate { get; set; }
    
    // Many-to-one-relationships
    [ForeignKey("CustomerId")]
    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;
    
    [ForeignKey("StatusTypeId")]
    public int StatusId { get; set; }
    public StatusTypeEntity Status { get; set; } = null!;
    
    [ForeignKey("ManagerId")] 
    public int ManagerId { get; set; }
    public UserEntity Manager { get; set; } = null!;
    
    [ForeignKey("ProductId")]
    public int ProductId { get; set; }
    public ProductEntity Product { get; set; } = null!;
}