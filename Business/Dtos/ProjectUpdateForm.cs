namespace Business.Dtos;

public record ProjectUpdateForm(
    string Title,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    
    string CustomerName,
    string CustomerEmail,
    
    string StatusName,
    
    string FirstName,
    string LastName,
    string Email,
    
    string ProductName,
    string ProductPrice
    );

