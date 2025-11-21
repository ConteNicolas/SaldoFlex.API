using MediatR;
using Microsoft.EntityFrameworkCore;
using SaldoFlex.API.Domain;
using SaldoFlex.API.Infrastructure.Persistence;
using SaldoFlex.API.Shared.Models;
using SaldoFlex.API.Shared.Utils;

namespace SaldoFlex.API.Features.Users.SignUp;

public record SignUpCommand(
    string Username,
    string Password,
    string? Email,
    string Firstname,
    string Lastname
) : IRequest<Result>;


public class SignUpCommandHandler : IRequestHandler<SignUpCommand, Result>
{
    private readonly ApplicationDbContext _context;

    public SignUpCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var exists = await _context.Users.AnyAsync(x => x.Username.ToLower() == request.Username.ToLower(), cancellationToken);

        if (exists)
        {
            return Result.Failure(new Error("User.SignUp.Exists", $"User with username {request.Username} already exists."));
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            Password = PasswordUtil.Hash(request.Password),
            Email = request.Email,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var account = new Account()
        {
            Id = Guid.NewGuid(),
            Firstname = request.Firstname,
            Lastname = request.Lastname,
            CreatedAt = DateTime.UtcNow,
            User = user
        };

        user.Account = account;

        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}