using FastEndpoints;
using MediatR;
using SaldoFlex.API.Domain;
using SaldoFlex.API.Domain.Enums;
using SaldoFlex.API.Shared.Attributes;

namespace SaldoFlex.API.Features.FinancialPlans.Delete;

[ResourceOwnershipRequired(entityType: typeof(FinancialPlan), routeValue: "Id")]
public class DeleteFinancialPlanEndpoint : Endpoint<DeleteFinancialPlanRequest>
{
    private ISender _sender;

    public DeleteFinancialPlanEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Delete("financial-plans/{Id}");
        Claims(nameof(ClaimsEnum.UserId));
    }

    public override async Task HandleAsync(DeleteFinancialPlanRequest req, CancellationToken ct)
    {
        var command = MapToCommand(req);
        var result = await _sender.Send(command, ct);

        if (result.IsFailure)
        {
            await Send.ResponseAsync(result.Error, StatusCodes.Status400BadRequest, ct);
            return;
        }

        await Send.NoContentAsync(ct);
    }

    private DeleteFinancialPlanCommand MapToCommand(DeleteFinancialPlanRequest req)
    {
        return new DeleteFinancialPlanCommand(req.Id);
    }
}
