using FastEndpoints;
using MediatR;

namespace SaldoFlex.API.Features.FinancialPlans.GetById;

public class GetFinancialPlanByIdEndpoint : Endpoint<GetFinancialPlanByIdRequest>
{
    private readonly ISender _sender;

    public GetFinancialPlanByIdEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get("financial-plans/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetFinancialPlanByIdRequest request, CancellationToken cancellationToken)
    {
        var query = MapToQuery(request);
        var result = await _sender.Send(query);

        if (result.IsFailure)
        {
            await Send.ResponseAsync(result.Error, StatusCodes.Status400BadRequest, cancellationToken);
            return;
        }

        await Send.ResponseAsync(result.Value, StatusCodes.Status200OK, cancellationToken);
    }

    private GetFinancialPlanByIdQuery MapToQuery(GetFinancialPlanByIdRequest request)
    {
        return new GetFinancialPlanByIdQuery(request.Id);
    }
}
