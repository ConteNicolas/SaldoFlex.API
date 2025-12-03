using SaldoFlex.API.Domain;

namespace SaldoFlex.API.Features.FinancialPlans.UpdateStatus;

public record UpdateFinancialPlanStatusRequest(Guid Id);

public record UpdateFinancialPlanStatusResponse(
    Guid Id, 
    DateTime UpdatedAt,
    string StatusDescription
);