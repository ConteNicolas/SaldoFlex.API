using SaldoFlex.API.Domain;

namespace SaldoFlex.API.Features.FinancialPlans.Update;

public record UpdateFinancialPlanRequest (
    Guid Id,
    string? Name,
    string? Description
);

public record UpdateFinancialPlanResponse(
    Guid Id,
    string Name,
    string? Description,
    string StatusDescription,
    DateTime CreatedAt, 
    DateTime UpdatedAt
);