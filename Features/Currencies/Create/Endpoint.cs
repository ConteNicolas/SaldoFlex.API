using FastEndpoints;
using MediatR;
using System.Net;

namespace SaldoFlex.API.Features.Currencies.Create;

public class CreateCurrencyEndpoint : Endpoint<CreateCurrencyRequest>
{

    private readonly ISender _sender;

    public CreateCurrencyEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post("currencies");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateCurrencyRequest request, CancellationToken cancellationToken)
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

    private CreateCurrencyCommand MapToCommand(CreateCurrencyRequest request)
    {
        return new CreateCurrencyCommand(request.Symbol, request.Code, request.Description, request.IsDefault);
    }
}
