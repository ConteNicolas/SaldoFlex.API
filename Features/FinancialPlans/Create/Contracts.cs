namespace SaldoFlex.API.Features.FinancialPlans.Create;

public record CreateFinancialPlanRequest(
    string Name, 
    string? Description
);

public record CreateFinancialPlanResponse(
    Guid Id,
    string Name,
    string? Description,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    List<CreateFinancialPlanTagsResponse> Tags
);

public record CreateFinancialPlanTagsResponse(
    Guid Id,
    string Name
);