namespace SaldoFlex.API.Features.Auth.SignUp;

public record SignUpRequest(
    string Username,
    string Password,
    string? Email,
    string Firstname,
    string Lastname
);