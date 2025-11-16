using FastEndpoints;
using MediatR;

namespace SaldoFlex.API.Features.Tags.Create;

public class CreateTagEndpoint : Endpoint<CreateTagRequest>
{
    private readonly ISender _sender;

    public CreateTagEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post("/tags");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateTagRequest request, CancellationToken cancellationToken)
    {
        var command = MapToCommand(request);
        var result = await _sender.Send(command, cancellationToken);
        
        if (result.IsFailure)
        {
            await Send.ResponseAsync(result.Error, StatusCodes.Status400BadRequest, cancellationToken);
        }

        await Send.OkAsync(result.Value, cancellationToken);
    }

    private CreateTagCommand MapToCommand(CreateTagRequest request) {
        return new CreateTagCommand(request.Name);
    }
}
