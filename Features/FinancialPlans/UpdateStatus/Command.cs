using MediatR;
using Microsoft.EntityFrameworkCore;
using SaldoFlex.API.Domain;
using SaldoFlex.API.Infrastructure.Persistence;
using SaldoFlex.API.Shared.Models;

namespace SaldoFlex.API.Features.FinancialPlans.UpdateStatus;

public record UpdateFinancialPlanStatusCommand(
    Guid Id
) : IRequest<Result<UpdateFinancialPlanStatusResponse>>;


public class UpdateFinancialPlanStatusCommandHandler : IRequestHandler<UpdateFinancialPlanStatusCommand, Result<UpdateFinancialPlanStatusResponse>>
{
    private readonly ApplicationDbContext _context;

    public UpdateFinancialPlanStatusCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<UpdateFinancialPlanStatusResponse>> Handle(UpdateFinancialPlanStatusCommand request, CancellationToken cancellationToken)
    {
        var plan = await _context.FinancialPlans.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (plan is null)
        {
            return Result.Failure<UpdateFinancialPlanStatusResponse>(new Error("FinancialPlan.Update.NotFound", "Financial plan not found."));
        }

        if (plan.Status == FinancialPlanStatusEnum.Active)
        {
            plan.Status = FinancialPlanStatusEnum.Archived;
        }
        else
        {
            plan.Status = FinancialPlanStatusEnum.Active;
        }

        _context.FinancialPlans.Update(plan);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Result.Success(MapToResponse(plan));
    }

    private UpdateFinancialPlanStatusResponse MapToResponse(FinancialPlan plan)
    {
        return new UpdateFinancialPlanStatusResponse(plan.Id, plan.UpdatedAt, plan.Status.ToString());
    }
}