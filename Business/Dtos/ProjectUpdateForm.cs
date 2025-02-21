namespace Business.Dtos;

public record ProjectUpdateForm(
    string Title,
    string? Description,
    string StartDate,
    string? EndDate,
    
    int CustomerId,
    int StatusId,
    int ManagerId,
    int ProductId
    );

