using MediatR;
using Microsoft.EntityFrameworkCore;
using SaldoFlex.API.Domain;
using SaldoFlex.API.Infrastructure.Persistence;
using SaldoFlex.API.Shared.Models;

namespace SaldoFlex.API.Features.FinancialPlans.Update;

public record UpdateFinancialPlanCommand(
    Guid Id,
    string? Name,
    string? Description
) : IRequest<Result<UpdateFinancialPlanResponse>>;

public class UpdateFinancialPlanCommandHandler : IRequestHandler<UpdateFinancialPlanCommand, Result<UpdateFinancialPlanResponse>>
{
    private readonly ApplicationDbContext _context;

    public UpdateFinancialPlanCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<UpdateFinancialPlanResponse>> Handle(UpdateFinancialPlanCommand request, CancellationToken cancellationToken)
    {
        var plan = await _context.FinancialPlans.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (plan is null)
        {
            return Result.Failure<UpdateFinancialPlanResponse>(new Error("FinancialPlan.Update.NotFound", "Financial plan not found."));>
        }

        plan.Name = request?.Name ?? plan.Name;
        plan.Description = request?.Description ?? plan.Description;

        plan.UpdatedAt = DateTime.UtcNow;

        _context.FinancialPlans.Update(plan);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(MapToResponse(plan));
    }

    private UpdateFinancialPlanResponse MapToResponse(FinancialPlan plan)
    {
        return new UpdateFinancialPlanResponse(plan.Id, plan.Name, plan?.Description);
    }
}