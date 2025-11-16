namespace SaldoFlex.API.Features.Tags.Create;

public class CreateTagRequestValidator : AbstractValidator<CreateTagRequest>
{
    public CreateTagRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
