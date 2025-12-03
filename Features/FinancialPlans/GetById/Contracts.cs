using SaldoFlex.API.Domain;

namespace SaldoFlex.API.Features.FinancialPlans.GetById;

public record GetFinancialPlanByIdRequest(Guid Id);

public record GetFinancialPlanByIdResponse(
    Guid Id, 
    string Name, 
    string? Description, 
    string StatusDescription,
    DateTime CreatedAt,
    DateTime UpdatedAt
);