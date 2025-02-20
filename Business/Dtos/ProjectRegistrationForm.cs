using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public record ProjectRegistrationForm(
    [Required]
    string Title,
    string Description,
    [Required]
    string StartDate,
    string? EndDate,
    
    int CustomerId,
    int StatusId,
    int ManagerId,
    int ProductId
    );

