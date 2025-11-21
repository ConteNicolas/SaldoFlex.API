using FastEndpoints;
using MediatR;
using SaldoFlex.API.Domain.Enums;

namespace SaldoFlex.API.Features.FinancialPlans.GetAll;

public class GetAllFinancialPlansEndpoint : Endpoint<GetAllFinancialPlansRequest>
{
    private readonly ISender _sender;

    public GetAllFinancialPlansEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get("financial-plans");
        Claims(nameof(ClaimsEnum.UserId));
    }

    public override async Task HandleAsync(GetAllFinancialPlansRequest request, CancellationToken cancellationToken)
    {
        var query = MapToQuery(request);

        var result = await _sender.Send(query, cancellationToken);

        await Send.OkAsync(result.Value, cancellationToken);
    }

    private GetAllFinancialPlansQuery MapToQuery(GetAllFinancialPlansRequest request)
    {
        return new GetAllFinancialPlansQuery(request.Page, request.PageSize, request?.Name);
    }
}
