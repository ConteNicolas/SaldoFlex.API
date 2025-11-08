namespace SaldoFlex.API.Features.Currencies.Create;

public record CreateCurrencyRequest(
  string Symbol,
  string Code,
  string Description
);

public record CreateCurrencyResponse(
  Guid Id,
  string Code,
  string Description,
  string Symbol
);

public class CreateCurrencyRequestValidator : AbstractValidator<CreateCurrencyRequest>
{
    public CreateCurrencyRequestValidator()
    {
        RuleFor(x => x.Symbol).NotEmpty();
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}