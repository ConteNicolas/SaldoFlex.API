using SaldoFlex.API.Domain.Abstractions;
using SaldoFlex.API.Shared.Enums;

namespace SaldoFlex.API.Infrastructure.Extension;

public static class QueryExtensions
{
    public static IQueryable<T> ApplyDateFilter<T>(
        this IQueryable<T> query,
        DateFilterTypes filterBy) where T : BaseEntity
    {
        return filterBy switch
        {
            DateFilterTypes.Today => query.Where(x => x.CreatedAt.Date == DateTime.UtcNow.Date),
            DateFilterTypes.LastWeek => query.Where(x => x.CreatedAt >= DateTime.UtcNow.AddDays(-7)),
            DateFilterTypes.LastMonth => query.Where(x => x.CreatedAt >= DateTime.UtcNow.AddMonths(-1)),
            _ => query
        };
    }

    public static IQueryable<T> ApplyOrderBy<T>(
        this IQueryable<T> query,
        OrderByTypes orderBy, OrderDirectionTypes direction = OrderDirectionTypes.Descending) where T : BaseEntity
    {
        return (orderBy, direction) switch
        {
            (OrderByTypes.CreationDate, OrderDirectionTypes.Ascending) => query.OrderBy(x => x.CreatedAt),
            (OrderByTypes.CreationDate, OrderDirectionTypes.Descending) => query.OrderByDescending(x => x.CreatedAt),

            (OrderByTypes.LastUpdate, OrderDirectionTypes.Ascending) => query.OrderBy(x => x.UpdatedAt),
            (OrderByTypes.LastUpdate, OrderDirectionTypes.Descending) => query.OrderByDescending(x => x.UpdatedAt),

            _ => query
        };

    }
}
