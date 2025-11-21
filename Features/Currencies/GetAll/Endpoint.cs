using FastEndpoints;
using MediatR;
using SaldoFlex.API.Domain.Enums;
using SaldoFlex.API.Shared.Models;

namespace SaldoFlex.API.Features.Currencies.GetAll;

public class GetAllCurrenciesEndpoint : Endpoint<GetAllCurrenciesRequest>
{
    private readonly ISender _sender;

    public GetAllCurrenciesEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get("currencies");
        Claims(nameof(ClaimsEnum.UserId));
    }

    public override async Task HandleAsync(GetAllCurrenciesRequest request, CancellationToken cancellationToken)
    {
        var query = MapToQuery(request);

        var result = await _sender.Send(query, cancellationToken);

        await Send.OkAsync(result.Value, cancellationToken);
    }

    private GetAllCurrenciesQuery MapToQuery(GetAllCurrenciesRequest request)
    {
        return new GetAllCurrenciesQuery(request.Code, request.Description, request.Symbol, request.Page, request.PageSize);
    }
}
