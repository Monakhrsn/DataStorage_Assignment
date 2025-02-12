namespace Business.Dtos;

public record ProjectRegistrationForm(
    string Title,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    
    int CustomerId,
    string CustomerName,
    string CustomerEmail,
    
    int StatusId,
    string StatusName,
    
    int UserId,
    string FirstName,
    string LastName,
    string Email,
    
    int ProductId,
    string ProductName,
    string ProductPrice
    );

