using Microsoft.EntityFrameworkCore;
using SaldoFlex.API.Domain.Abstractions;
using SaldoFlex.API.Domain.Abstractions.Interfaces;
using SaldoFlex.API.Infrastructure.Persistence;
using SaldoFlex.API.Shared.Context;
using SaldoFlex.API.Shared.Models;

namespace SaldoFlex.API.Shared.Services;

public class ResourceOwnershipService : IResourceOwnershipService
{
    private readonly IUserContext _userContext;
    private readonly ApplicationDbContext _dbContext;

    public ResourceOwnershipService(IUserContext userContext, ApplicationDbContext dbContext)
    {
        _userContext = userContext;
        _dbContext = dbContext;
    }

    public async Task<Result> ValidateOwnership<TEntity>(Guid resourceId) where TEntity : BaseEntity, IUserOwnedEntity
    {
        var userId = _userContext.GetUserId();

        if (userId is null)
        {
            return Result.Failure(new Error("User.HasOwnership.NotFound", "Cannot get user from context."));
        }

        var isOwner = await _dbContext.Set<TEntity>().AnyAsync(x => x.UserId == userId && x.Id == resourceId, new CancellationToken());

        return isOwner ? Result.Success() : Result.Failure(new Error("User.HasOwnership.InvalidOperation", "Do not own this resource."));
    }
}
