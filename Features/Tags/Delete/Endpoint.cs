using FastEndpoints;
using MediatR;
using SaldoFlex.API.Domain;
using SaldoFlex.API.Domain.Enums;
using SaldoFlex.API.Shared.Attributes;

namespace SaldoFlex.API.Features.Tags.Delete;

[ResourceOwnershipRequired(entityType: typeof(Tag), routeValue: "Id")]
public class DeleteTagEndpoint : Endpoint<DeleteTagRequest>
{
    private readonly ISender _sender;

    public DeleteTagEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Delete("/tags/{Id}");
        Claims(nameof(ClaimsEnum.UserId));
    }

    public override async Task HandleAsync(DeleteTagRequest req, CancellationToken cancellationToken)
    {
        var command = MapToCommand(req);
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            await Send.ResponseAsync(result.Error, StatusCodes.Status400BadRequest, cancellationToken);
            return;
        }

        await Send.NoContentAsync(cancellationToken);
    }

    private DeleteTagCommand MapToCommand(DeleteTagRequest request)
    {
        return new DeleteTagCommand(request.Id);
    }
}
