using MediatR;
using Microsoft.EntityFrameworkCore;
using SaldoFlex.API.Domain;
using SaldoFlex.API.Infrastructure.Persistence;
using SaldoFlex.API.Shared.Context;
using SaldoFlex.API.Shared.Models;

namespace SaldoFlex.API.Features.FinancialPlans.Create;

public record CreateFinancialPlanCommand(
    string Name,
    string? Description
) : IRequest<Result<CreateFinancialPlanResponse>>;

public class CreateFinancialPlanCommandHandler : IRequestHandler<CreateFinancialPlanCommand, Result<CreateFinancialPlanResponse>>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserContext _userContext;

    public CreateFinancialPlanCommandHandler(ApplicationDbContext context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<Result<CreateFinancialPlanResponse>> Handle(CreateFinancialPlanCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId().GetValueOrDefault();

        var plan = new FinancialPlan()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request?.Description,
            CreatedAt = DateTime.UtcNow,
            UserId = userId,
            Status = FinancialPlanStatusEnum.Active,
            Origin = FinancialPlanOriginEnum.UserCreated
        };

        var exists = await _context.FinancialPlans.AnyAsync(x => x.Name.ToLower() == request!.Name.ToLower(), cancellationToken);

        if (exists)
        {
            return Result.Failure<CreateFinancialPlanResponse>(new Error("FinancialPlan.Create.Exists", "Financial plan already exists."));
        }

        var scene = new FinancialScene()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            UserId = userId
        };

        plan.FinancialSceneId = scene.Id;
        plan.FinancialScene = scene;

        scene.Groups.Add(new FinancialSceneGroup()
        {
            Id = Guid.NewGuid(),
            Name = "General expenses",
            FinancialSceneId = scene.Id,
            FinancialScene = scene,
            CreatedAt = DateTime.UtcNow,
            UserId = userId
        });


        await _context.FinancialPlans.AddAsync(plan, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success<CreateFinancialPlanResponse>(MapToResponse(plan));
    }

    private CreateFinancialPlanResponse MapToResponse(FinancialPlan plan)
    {
        return new CreateFinancialPlanResponse(plan.Id, plan.Name, plan?.Description, plan.CreatedAt, plan.UpdatedAt, plan.Status, plan.Status.ToString());
    }
}