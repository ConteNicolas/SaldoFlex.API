using FastEndpoints;
using MediatR;

namespace SaldoFlex.API.Features.Currencies.Delete;

public class DeleteCurrencyEndpoint : Endpoint<DeleteCurrencyRequest>
{
    private readonly ISender _sender;

    public DeleteCurrencyEndpoint(ISender sender)
    {
        _sender = sender;
    }


    public override void Configure()
    {
        Delete("currencies/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteCurrencyRequest request, CancellationToken cancellationToken)
    {
        var command = MapToCommand(request);
        var result = await _sender.Send(command, cancellationToken);
        
        if (result.IsFailure)
        {
            await Send.ResponseAsync(result.Error, StatusCodes.Status400BadRequest, cancellationToken);
        }

        await Send.NoContentAsync();
    }

    private DeleteCurrencyCommand MapToCommand(DeleteCurrencyRequest request)
    {
        return new DeleteCurrencyCommand(request.Id);
    }
}
