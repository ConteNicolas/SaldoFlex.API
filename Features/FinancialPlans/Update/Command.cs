using MediatR;
using Microsoft.EntityFrameworkCore;
using SaldoFlex.API.Domain;
using SaldoFlex.API.Infrastructure.Persistence;
using SaldoFlex.API.Shared.Context;
using SaldoFlex.API.Shared.Models;

namespace SaldoFlex.API.Features.FinancialPlans.Update;

public record UpdateFinancialPlanCommand(
    Guid Id,
    string? Name,
    string? Description,
    FinancialPlanStatusEnum Status
) : IRequest<Result<UpdateFinancialPlanResponse>>;

public class UpdateFinancialPlanCommandHandler : IRequestHandler<UpdateFinancialPlanCommand, Result<UpdateFinancialPlanResponse>>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserContext _userContext;

    public UpdateFinancialPlanCommandHandler(ApplicationDbContext context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<Result<UpdateFinancialPlanResponse>> Handle(UpdateFinancialPlanCommand request, CancellationToken cancellationToken)
    {
        var plan = await _context.FinancialPlans
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (plan is null)
        {
            return Result.Failure<UpdateFinancialPlanResponse>(new Error("FinancialPlan.Update.NotFound", "Financial plan not found."));
        }

        if (plan.Name.ToLower() == request.Name?.ToLower() && plan.Description == request?.Description && plan.Status == request.Status)
        {
            return Result.Failure<UpdateFinancialPlanResponse>(new Error("FinancialPlan.Update.NoChanges", "There no changes to update."));
        }

        var userId = _userContext.GetUserId().GetValueOrDefault();

        if (plan.Status != request.Status)
        {
            plan.Status = request.Status;
        }

        plan.Name = request.Name ?? plan.Name;
        plan.Description = request?.Description ?? plan.Description;

        plan.UpdatedAt = DateTime.UtcNow;

        _context.FinancialPlans.Update(plan);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(MapToResponse(plan));
    }

    private UpdateFinancialPlanResponse MapToResponse(FinancialPlan plan)
    {
        return new UpdateFinancialPlanResponse(plan.Id, plan.Name, plan?.Description, plan.CreatedAt, plan.UpdatedAt, plan.Status, plan.Status.ToString());
    }
}