namespace SaldoFlex.API.Features.Currencies.Create;

public record CreateCurrencyRequest(
  string Symbol,
  string Code,
  string Description,
  bool IsDefault
);

public record CreateCurrencyResponse(
  Guid Id,
  string Code,
  string Description,
  string Symbol,
  bool IsDefault
);