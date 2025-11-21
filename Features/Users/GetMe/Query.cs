using MediatR;
using Microsoft.EntityFrameworkCore;
using SaldoFlex.API.Domain;
using SaldoFlex.API.Infrastructure.Persistence;
using SaldoFlex.API.Shared.Context;
using SaldoFlex.API.Shared.Models;

namespace SaldoFlex.API.Features.Users.GetMe;

public record GetMeQuery() : IRequest<Result<GetMeResponse>>;

public class GetMeQueryHandler : IRequestHandler<GetMeQuery, Result<GetMeResponse>>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserContext _userContext;

    public GetMeQueryHandler(ApplicationDbContext context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<Result<GetMeResponse>> Handle(GetMeQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId().GetValueOrDefault();

        var user = await _context.Users
            .Include(x => x.Account)
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<GetMeResponse>(new Error("User.GetMe.NotFound", "User not found."));
        }

        return Result.Success(MapToResponse(user));
    }

    private GetMeResponse MapToResponse(User user)
    {
        var alias = string.Concat(user.Account.Firstname[0], user.Account.Lastname[0]);

        return new GetMeResponse(user.Username, user.Account.Firstname, user.Account.Lastname, alias, user.Email);
    }
}