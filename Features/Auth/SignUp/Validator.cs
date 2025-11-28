namespace SaldoFlex.API.Features.Auth.SignUp;

public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
{
    public SignUpRequestValidator()
    {
        RuleFor(x => x.Username).NotEmpty().NotNull();
        RuleFor(x => x.Password).NotEmpty().NotNull().MinimumLength(8);
        RuleFor(x => x.Firstname).NotEmpty().NotNull();
        RuleFor(x => x.Lastname).NotEmpty().NotNull();
    }
}
