using MediatR;
using Microsoft.EntityFrameworkCore;
using SaldoFlex.API.Domain;
using SaldoFlex.API.Infrastructure.Persistence;
using SaldoFlex.API.Shared.Models;

namespace SaldoFlex.API.Features.FinancialPlans.GetById;

public record GetFinancialPlanByIdQuery(
    Guid Id
) : IRequest<Result<GetFinancialPlanByIdResponse>>;

public class GetFinancialPlanByIdQueryHandler : IRequestHandler<GetFinancialPlanByIdQuery, Result<GetFinancialPlanByIdResponse>>
{
    private readonly ApplicationDbContext _context;

    public GetFinancialPlanByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<GetFinancialPlanByIdResponse>> Handle(GetFinancialPlanByIdQuery request, CancellationToken cancellationToken)
    {
        var plan = await _context.FinancialPlans.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (plan is null)
        {
            return Result.Failure<GetFinancialPlanByIdResponse>(new Error("FinancialPlan.GetById.NotFound", "Financial plan not found."));
        }

        return Result.Success(MapToResponse(plan));
    }

    private GetFinancialPlanByIdResponse MapToResponse(FinancialPlan plan)
    {
        var tags = MapTags(plan);
        return new GetFinancialPlanByIdResponse(plan.Id, plan.Name, plan?.Description, tags);
    }

    private List<GetFinancialPlanTagByIdResponse> MapTags(FinancialPlan plan)
    {
        return plan.Tags.Select(x => new GetFinancialPlanTagByIdResponse(x.Id, x.Name)).ToList();
    }
}