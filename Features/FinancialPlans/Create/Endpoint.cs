using FastEndpoints;
using MediatR;
using SaldoFlex.API.Domain.Enums;

namespace SaldoFlex.API.Features.FinancialPlans.Create;

public class CreateFinancialPlanEndpoint : Endpoint<CreateFinancialPlanRequest>
{
    private readonly ISender _sender;

    public CreateFinancialPlanEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post("financial-plans");
        Claims(nameof(ClaimsEnum.UserId));
    }

    public override async Task HandleAsync(CreateFinancialPlanRequest req, CancellationToken ct)
    {
        var command = MapToCommand(req);
        var result = await _sender.Send(command, ct);

        if (result.IsFailure)
        {
            await Send.ResponseAsync(result.Error, StatusCodes.Status400BadRequest, ct);
            return;
        }

        await Send.ResponseAsync(result.Value, StatusCodes.Status201Created, ct);
    }

    private CreateFinancialPlanCommand MapToCommand(CreateFinancialPlanRequest request)
    {
        return new CreateFinancialPlanCommand(request.Name, request?.Description);
    }
}
