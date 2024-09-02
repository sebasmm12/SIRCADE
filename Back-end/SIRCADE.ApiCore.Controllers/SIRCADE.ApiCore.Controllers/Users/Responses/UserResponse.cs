namespace SIRCADE.ApiCore.Controllers.Users.Responses;

public record UserResponse(
    int Id, 
    string Nsa, 
    string FullName, 
    string Grade, 
    string Unity,
    bool Active,
    string Role);