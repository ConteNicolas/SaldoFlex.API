namespace SaldoFlex.API.Features.Currencies.Create;

public record CreateCurrencyRequest(
  string Symbol,
  string Code,
  string Description
);

public record CreateCurrencyResponse(
  Guid Id,
  string Symbol,
  string Code,
  string Description,
  DateTime CreatedAt,
  DateTime UpdatedAt
);