using SaldoFlex.API.Domain;
using SaldoFlex.API.Shared.Enums;

namespace SaldoFlex.API.Features.FinancialPlans.GetAll;

public record GetAllFinancialPlansRequest(
    int Page = 1,
    int PageSize = 10,
    string? Name = null,
    DateFilterTypes DateFilter = DateFilterTypes.None,
    OrderByTypes OrderBy = OrderByTypes.CreationDate,
    OrderDirectionTypes OrderDirection = OrderDirectionTypes.Descending
);

public record GetAllFinancialPlansResponse(
    Guid Id,
    string Name,
    string? Description,
    string StatusDescription,
    DateTime CreatedAt,
    DateTime UpdatedAt
);