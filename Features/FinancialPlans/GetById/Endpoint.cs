using FastEndpoints;
using MediatR;
using SaldoFlex.API.Domain;
using SaldoFlex.API.Domain.Enums;
using SaldoFlex.API.Shared.Attributes;

namespace SaldoFlex.API.Features.FinancialPlans.GetById;

[ResourceOwnershipRequired(entityType: typeof(FinancialPlan), routeValue: "Id")]
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
        Claims(nameof(ClaimsEnum.UserId));
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
