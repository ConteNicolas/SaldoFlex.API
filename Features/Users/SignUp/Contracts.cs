namespace SaldoFlex.API.Features.Users.SignUp;

public record SignUpRequest(
    string Username,
    string Password,
    string? Email,
    string Firstname,
    string Lastname
);