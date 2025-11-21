using FastEndpoints;
using MediatR;
using SaldoFlex.API.Domain;
using SaldoFlex.API.Domain.Enums;
using SaldoFlex.API.Shared.Attributes;

namespace SaldoFlex.API.Features.Currencies.Delete;


[ResourceOwnershipRequired(entityType: typeof(Currency), routeValue: "Id")]
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
        Claims(nameof(ClaimsEnum.UserId));
    }

    public override async Task HandleAsync(DeleteCurrencyRequest request, CancellationToken cancellationToken)
    {
        var command = MapToCommand(request);
        var result = await _sender.Send(command, cancellationToken);
        
        if (result.IsFailure)
        {
            await Send.ResponseAsync(result.Error, StatusCodes.Status400BadRequest, cancellationToken);
            return;
        }

        await Send.NoContentAsync();
    }

    private DeleteCurrencyCommand MapToCommand(DeleteCurrencyRequest request)
    {
        return new DeleteCurrencyCommand(request.Id);
    }
}
