using SaldoFlex.API.Shared.Enums;

namespace SaldoFlex.API.Features.Tags.GetAll;

public record GetAllTagsRequest(
    int Page = 1,
    int PageSize = 10,
    string? Name = null,
    DateFilterTypes DateFilter = DateFilterTypes.None,
    OrderByTypes OrderBy = OrderByTypes.CreationDate,
    OrderDirectionTypes OrderDirection = OrderDirectionTypes.Descending
);

public record GetAllTagsResponse(
    Guid Id, 
    string Name,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
