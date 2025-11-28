using FastEndpoints.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SaldoFlex.API.Infrastructure.Persistence;
using SaldoFlex.API.Shared.Models;
using SaldoFlex.API.Shared.Utils;

namespace SaldoFlex.API.Features.Auth.SignIn;

public record SignInCommand(
    string Username,
    string Password
) : IRequest<Result<SignInResponse>>;

public class SignInCommandHandler : IRequestHandler<SignInCommand, Result<SignInResponse>>
{
    private readonly ApplicationDbContext _context;

    public SignInCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<SignInResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var hashedPassword = PasswordUtil.Hash(request.Password);

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == request.Username && x.Password == hashedPassword, cancellationToken);


        if (user is null)
        {
            return Result.Failure<SignInResponse>(new Error("User.SignIn.NotFound", "User not found."));
        }

        var token = JwtBearer.CreateToken(opt =>
        {
            opt.ExpireAt = DateTime.UtcNow.AddDays(1);
            opt.User.Claims.Add(("UserId", user.Id.ToString()));
        });

        return Result.Success(MapToResponse(token));
    }

    private SignInResponse MapToResponse(string token)
    {
        return new SignInResponse(token);
    }
}