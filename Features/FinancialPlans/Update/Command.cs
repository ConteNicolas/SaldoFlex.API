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
    string? Description
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
            .Include(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (plan is null)
        {
            return Result.Failure<UpdateFinancialPlanResponse>(new Error("FinancialPlan.Update.NotFound", "Financial plan not found."));
        }

        var userId = _userContext.GetUserId().GetValueOrDefault();

        var existingDuplicateTag = plan.Tags.FirstOrDefault(x => x.Name.ToLower() == "duplicate");
        if (existingDuplicateTag is not null && request?.Name != plan.Name)
        {
            plan.Tags.Remove(existingDuplicateTag);
        }

        var alreadyExistPlanWithSameName = await _context.FinancialPlans.AnyAsync(x => x.Name.ToLower() == request!.Name.ToLower() && x.Id != request.Id, cancellationToken);
        if (alreadyExistPlanWithSameName && existingDuplicateTag is null)
        {
            var duplicateTag = await _context.Tags.FirstOrDefaultAsync(x => x.Name == "Duplicate" && x.UserId == userId, cancellationToken);

            if (duplicateTag is null)
            {

                duplicateTag = new Tag()
                {
                    Id = Guid.NewGuid(),
                    Name = "Duplicate",
                    CreatedAt = DateTime.UtcNow,
                    UserId = userId
                };

                await _context.Tags.AddAsync(duplicateTag, cancellationToken);
            }

            plan.Tags.Add(duplicateTag);
        }

        plan.Name = request.Name;
        plan.Description = request?.Description;

        plan.UpdatedAt = DateTime.UtcNow;

        _context.FinancialPlans.Update(plan);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(MapToResponse(plan));
    }

    private UpdateFinancialPlanResponse MapToResponse(FinancialPlan plan)
    {
        var tags = MapTags(plan);
        return new UpdateFinancialPlanResponse(plan.Id, plan.Name, plan?.Description, plan.CreatedAt, plan.UpdatedAt, tags);
    }

    private List<UpdateFinancialPlanTagResponse> MapTags(FinancialPlan plan)
    {
        return plan?.Tags.Select(x => new UpdateFinancialPlanTagResponse(x.Id, x.Name)).ToList() ?? new List<UpdateFinancialPlanTagResponse>();
    }
}