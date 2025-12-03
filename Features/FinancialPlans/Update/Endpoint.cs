using FastEndpoints;
using MediatR;
using SaldoFlex.API.Domain;
using SaldoFlex.API.Domain.Enums;
using SaldoFlex.API.Shared.Attributes;

namespace SaldoFlex.API.Features.FinancialPlans.Update;

[ResourceOwnershipRequired(entityType: typeof(FinancialPlan), routeValue: "Id")]
public class UpdateFinancialPlanEndpoint : Endpoint<UpdateFinancialPlanRequest>
{
    private readonly ISender _sender;

    public UpdateFinancialPlanEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Put("financial-plans/{Id}");
        Claims(nameof(ClaimsEnum.UserId));
    }

    public override async Task HandleAsync(UpdateFinancialPlanRequest req, CancellationToken ct)
    {
        var command = MapToCommand(req);
        var result = await _sender.Send(command, ct);

        if (result.IsFailure)
        {
            await Send.ResponseAsync(result.Error, StatusCodes.Status400BadRequest, ct);
            return;
        }

        await Send.OkAsync(result.Value, ct);
    }

    private UpdateFinancialPlanCommand MapToCommand(UpdateFinancialPlanRequest req)
    {
        return new UpdateFinancialPlanCommand(req.Id, req.Name, req.Description);
    }
}
