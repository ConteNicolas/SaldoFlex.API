namespace SaldoFlex.API.Features.FinancialPlans.Create;

public class CreateFinancialPlanRequestValidator : AbstractValidator<CreateFinancialPlanRequest>
{
    public CreateFinancialPlanRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}