using FastEndpoints;
using MediatR;
using SaldoFlex.API.Domain;
using SaldoFlex.API.Domain.Enums;
using SaldoFlex.API.Shared.Attributes;

namespace SaldoFlex.API.Features.FinancialPlans.UpdateStatus;


[ResourceOwnershipRequired(entityType: typeof(FinancialPlan), routeValue: "Id")]
public class UpdateFinancialPlanStatusEndpoint : Endpoint<UpdateFinancialPlanStatusRequest>
{
    private readonly ISender _sender;

    public UpdateFinancialPlanStatusEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Put("/financial-plans/{Id}/status");
        Claims(nameof(ClaimsEnum.UserId));
    }

    public override async Task HandleAsync(UpdateFinancialPlanStatusRequest req, CancellationToken ct)
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

    private UpdateFinancialPlanStatusCommand MapToCommand(UpdateFinancialPlanStatusRequest req)
    {
        return new UpdateFinancialPlanStatusCommand(req.Id);
    }
}
