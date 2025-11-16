namespace SaldoFlex.API.Features.Currencies.Update;

public record UpdateCurrencyRequest(
    Guid Id,
    string? Description,
    string? Symbol,
    string? Code
);

public record UpdateCurrencyResponse(
    Guid Id,
    string Description,
    string Symbol,
    string Code
);