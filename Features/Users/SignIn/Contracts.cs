namespace SaldoFlex.API.Features.Users.SignIn;

public record SignInRequest(
    string Username,
    string Password
);

public record SignInResponse(
    string Token
);