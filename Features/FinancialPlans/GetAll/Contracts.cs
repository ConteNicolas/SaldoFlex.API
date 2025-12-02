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
    DateTime CreatedAt,
    DateTime UpdatedAt,
    List<GetAllFinancialPlanTagsResponse> Tags
);

public record GetAllFinancialPlanTagsResponse(
    Guid Id,
    string Name
);