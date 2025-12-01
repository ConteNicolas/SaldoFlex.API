namespace SaldoFlex.API.Features.FinancialPlans.GetById;

public record GetFinancialPlanByIdRequest(Guid Id);

public record GetFinancialPlanByIdResponse(
    Guid Id, 
    string name, 
    string? description, 
    List<GetFinancialPlanTagByIdResponse> tags
);

public record GetFinancialPlanTagByIdResponse(Guid Id, string name);