namespace SaldoFlex.API.Features.Currencies.Update;

public class UpdateCurrencyRequestValidator : AbstractValidator<UpdateCurrencyRequest>
{
    public UpdateCurrencyRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}