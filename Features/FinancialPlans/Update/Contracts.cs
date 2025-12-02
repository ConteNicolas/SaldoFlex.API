namespace SaldoFlex.API.Features.FinancialPlans.Update;

public record UpdateFinancialPlanRequest (
    Guid Id,
    string? Name,
    string? Description
);

public record UpdateFinancialPlanResponse(
    Guid Id,
    string? Name,
    string? Description,
    DateTime CreatedAt, 
    DateTime UpdatedAt,
    List<UpdateFinancialPlanTagResponse> Tags
);

public record UpdateFinancialPlanTagResponse(
    Guid Id,
    string Name
);