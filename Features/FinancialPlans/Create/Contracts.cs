using SaldoFlex.API.Domain;

namespace SaldoFlex.API.Features.FinancialPlans.Create;

public record CreateFinancialPlanRequest(
    string Name, 
    string? Description
);

public record CreateFinancialPlanResponse(
    Guid Id,
    string Name,
    string? Description,
    string StatusDescription,
    DateTime CreatedAt,
    DateTime UpdatedAt
);