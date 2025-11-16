namespace SaldoFlex.API.Features.Tags.GetAll;

public record GetAllTagsRequest(
    int Page = 1,
    int PageSize = 10,
    string? Name = null
);

public record GetAllTagsResponse(
    Guid Id, 
    string Name
);
