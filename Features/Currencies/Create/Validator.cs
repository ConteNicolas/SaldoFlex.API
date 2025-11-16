namespace SaldoFlex.API.Features.Currencies.Create;

public class CreateCurrencyRequestValidator : AbstractValidator<CreateCurrencyRequest>
{
    public CreateCurrencyRequestValidator()
    {
        RuleFor(x => x.Symbol).NotEmpty();
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}