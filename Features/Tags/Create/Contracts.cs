namespace SaldoFlex.API.Features.Tags.Create;

public record CreateTagRequest(
    string Name
);

public record CreateTagResponse(
    Guid Id,
    string Name
);