using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public record ProjectRegistrationForm(
    [Required]
    string Title,
    string Description,
    [Required]
    DateTime? StartDate,
    DateTime? EndDate,
    
    int CustomerId,
    int StatusId,
    int UserId,
    int ProductId
    );

