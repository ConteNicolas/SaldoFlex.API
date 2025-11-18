using FastEndpoints;
using MediatR;

namespace SaldoFlex.API.Features.FinancialPlans.Delete;

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
        AllowAnonymous();
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
