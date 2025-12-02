using SaldoFlex.API.Domain;

namespace SaldoFlex.API.Features.FinancialPlans.Update;

public record UpdateFinancialPlanRequest (
    Guid Id,
    string? Name,
    string? Description,
    FinancialPlanStatusEnum Status = FinancialPlanStatusEnum.Active
);

public record UpdateFinancialPlanResponse(
    Guid Id,
    string? Name,
    string? Description,
    DateTime CreatedAt, 
    DateTime UpdatedAt,
    FinancialPlanStatusEnum Status, 
    string StatusDescription
);