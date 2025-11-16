using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc.Routing;

namespace SaldoFlex.API.Features.Tags.Update;

public class UpdateTagEndpoint : Endpoint<UpdateTagRequest>
{
    private readonly ISender _sender;

    public UpdateTagEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Put("tags");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateTagRequest req, CancellationToken ct)
    {
        var command = MapToCommand(req);
        var result = await _sender.Send(command, ct);

        if (result.IsFailure)
        {
            await Send.ResponseAsync(result.Error, StatusCodes.Status400BadRequest, ct);
            return;
        }

        await Send.ResponseAsync(result.Value, StatusCodes.Status200OK, ct);
    }

    private UpdateTagCommand MapToCommand(UpdateTagRequest request)
    {
        return new UpdateTagCommand(request.Id, request.Name);
    }
}
