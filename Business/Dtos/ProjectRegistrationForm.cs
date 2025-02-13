namespace Business.Dtos;

public record ProjectRegistrationForm(
    string Title,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    
    int CustomerId,
    int StatusId,
    int UserId,
    int ProductId
    );

