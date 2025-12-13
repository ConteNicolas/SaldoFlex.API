using FastEndpoints;
using MediatR;
using SaldoFlex.API.Domain.Enums;

namespace SaldoFlex.API.Features.Tags.GetAll;

public class GetAllTagsEndpoint : Endpoint<GetAllTagsRequest>
{
    private readonly ISender _sender;

    public GetAllTagsEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get("/tags");
        Claims(nameof(ClaimsEnum.UserId));
    }

    public override async Task HandleAsync(GetAllTagsRequest request, CancellationToken cancellationToken)
    {
        var query = MapToQuery(request);

        var result = await _sender.Send(query, cancellationToken);

        await Send.OkAsync(result.Value, cancellationToken);
    }

    private GetAllTagsQuery MapToQuery(GetAllTagsRequest request)
    {
        return new GetAllTagsQuery(request.Page, request.PageSize, request.Name, request.DateFilter, request.OrderBy, request.OrderDirection);
    }
}
