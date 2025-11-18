using MediatR;
using Microsoft.EntityFrameworkCore;
using SaldoFlex.API.Infrastructure.Persistence;
using SaldoFlex.API.Shared.Models;

namespace SaldoFlex.API.Features.FinancialPlans.Delete;

public record DeleteFinancialPlanCommand(Guid Id) : IRequest<Result>;

public class DeleteFinancialPlanCommandHandler : IRequestHandler<DeleteFinancialPlanCommand, Result>
{
    private readonly ApplicationDbContext _context;

    public DeleteFinancialPlanCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Result> Handle(DeleteFinancialPlanCommand request, CancellationToken cancellationToken)
    {
        var plan = await _context.FinancialPlans.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (plan is null)
        {
            return Result.Failure(new Error("FinancialPlan.Delete.NotFound", "Financial plan not found."));
        }
        
        _context.FinancialPlans.Remove(plan);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}