namespace Business.Dtos;

public record User(
    int Id, 
    string FirstName,
    string LastName,
    string Email,
    string RoleName,
    int RoleId
    );