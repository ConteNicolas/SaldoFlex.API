using MediatR;
using Microsoft.EntityFrameworkCore;
using SaldoFlex.API.Domain;
using SaldoFlex.API.Infrastructure.Persistence;
using SaldoFlex.API.Shared.Models;

namespace SaldoFlex.API.Features.FinancialPlans.Create;

public record CreateFinancialPlanCommand(
    string Name,
    string? Description
) : IRequest<Result<CreateFinancialPlanResponse>>;

public class CreateFinancialPlanCommandHandler : IRequestHandler<CreateFinancialPlanCommand, Result<CreateFinancialPlanResponse>>
{
    private readonly ApplicationDbContext _context;
    
    public CreateFinancialPlanCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<CreateFinancialPlanResponse>> Handle(CreateFinancialPlanCommand request, CancellationToken cancellationToken)
    {
        var exists = await _context.FinancialPlans.AnyAsync(x => x.Name == request.Name, cancellationToken);

        if (exists)
        {
            return Result.Failure<CreateFinancialPlanResponse>(new Error("FinancialPlan.Create.Exists", "Financial plan already exists."));
        }

        var plan = new FinancialPlan()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request?.Description
        };

        var scene = new FinancialScene()
        {
            Id = Guid.NewGuid()
        };

        plan.FinancialSceneId = scene.Id;
        plan.FinancialScene = scene;

        scene.Groups.Add(new FinancialSceneGroup()
        {
            Id = Guid.NewGuid(),
            Name = "General expenses",
            FinancialSceneId = scene.Id,
            FinancialScene = scene
        });


        await _context.FinancialPlans.AddAsync(plan, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success<CreateFinancialPlanResponse>(MapToResponse(plan));
    }

    private CreateFinancialPlanResponse MapToResponse(FinancialPlan plan)
    {
        return new CreateFinancialPlanResponse(plan.Id, plan.Name, plan?.Description);
    }
}