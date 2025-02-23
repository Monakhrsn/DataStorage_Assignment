namespace Business.Dtos;

public record UserRegistrationForm(
    string FirstName, 
    string LastName, 
    string Email,
    int RoleId
    );
