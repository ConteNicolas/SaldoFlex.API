using FastEndpoints;
using MediatR;
using SaldoFlex.API.Domain.Enums;

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
        Claims(nameof(ClaimsEnum.UserId));
    }

    public override async Task HandleAsync(CreateTagRequest request, CancellationToken cancellationToken)
    {
        var command = MapToCommand(request);
        var result = await _sender.Send(command, cancellationToken);
        
        if (result.IsFailure)
        {
            await Send.ResponseAsync(result.Error, StatusCodes.Status400BadRequest, cancellationToken);
            return;
        }

        await Send.OkAsync(result.Value, cancellationToken);
    }

    private CreateTagCommand MapToCommand(CreateTagRequest request) {
        return new CreateTagCommand(request.Name);
    }
}
