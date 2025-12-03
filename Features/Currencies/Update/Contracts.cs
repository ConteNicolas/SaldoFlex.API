namespace SaldoFlex.API.Features.Currencies.Update;

public record UpdateCurrencyRequest(
    Guid Id,
    string? Description,
    string? Symbol,
    string? Code,
    bool? IsDefault
);

public record UpdateCurrencyResponse(
    Guid Id,
    string Description,
    string Symbol,
    string Code,
    bool IsDefault,
    DateTime CreatedAt,
    DateTime UpdatedAt
);