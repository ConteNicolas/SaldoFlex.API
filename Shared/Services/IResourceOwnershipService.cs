using SaldoFlex.API.Domain.Abstractions;
using SaldoFlex.API.Domain.Abstractions.Interfaces;
using SaldoFlex.API.Shared.Models;

namespace SaldoFlex.API.Shared.Services;

public interface IResourceOwnershipService
{
    public Task<Result> ValidateOwnership<TEntity>(Guid resourceId) where TEntity : BaseEntity, IUserOwnedEntity;
}
