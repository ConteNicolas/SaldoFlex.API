namespace SaldoFlex.API.Features.Tags.Update;

public record UpdateTagRequest(Guid Id, string Name);

public record UpdateTagResponse(Guid Id, string Name);
