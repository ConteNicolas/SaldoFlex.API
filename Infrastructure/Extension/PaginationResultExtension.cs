using Microsoft.EntityFrameworkCore;
using SaldoFlex.API.Shared.Models;

namespace SaldoFlex.API.Infrastructure.Extension;

public static class PaginationResultExtension
{
    public static Task<PaginatedResult<TDestination>> ToPaginatedResultAsync<TDestination>(
        this IQueryable<TDestination> queryable, int currentPage, int pageSize, CancellationToken cancellationToken)
        where TDestination : class
    {
        return PaginatedResult<TDestination>.CreateAsync(queryable.AsNoTracking(), currentPage, pageSize, cancellationToken);
    }
}
