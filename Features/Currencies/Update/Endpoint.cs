using FastEndpoints;
using MediatR;
using SaldoFlex.API.Domain;
using SaldoFlex.API.Domain.Enums;
using SaldoFlex.API.Shared.Attributes;

namespace SaldoFlex.API.Features.Currencies.Update;

[ResourceOwnershipRequired(entityType: typeof(Currency), routeValue: "Id")]
public class UpdateCurrencyEndpoint : Endpoint<UpdateCurrencyRequest>
{
    private readonly ISender _sender;

    public UpdateCurrencyEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Put("currencies/{Id}");
        Claims(nameof(ClaimsEnum.UserId));
    }

    public override async Task HandleAsync(UpdateCurrencyRequest request, CancellationToken cancellationToken)
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

    private UpdateCurrencyCommand MapToCommand(UpdateCurrencyRequest request)
    {
        return new UpdateCurrencyCommand(request.Id, request?.Symbol, request?.Code, request?.Description, request?.IsDefault);
    }
}