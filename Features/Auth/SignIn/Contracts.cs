namespace SaldoFlex.API.Features.Auth.SignIn;

public record SignInRequest(
    string Username,
    string Password
);

public record SignInResponse(
    string Token
);