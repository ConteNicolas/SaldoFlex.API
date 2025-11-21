namespace SaldoFlex.API.Features.Users.GetMe;

public record GetMeResponse(
    string Username,
    string Firstname,
    string Lastname,
    string Alias,
    string? Email
);