namespace Business.Dtos;

public record UserUpdateForm(
    string FirstName,
    string LastName,
    string Email,
    string RoleName
    );
