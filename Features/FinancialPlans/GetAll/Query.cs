using MediatR;
using SaldoFlex.API.Infrastructure.Extension;
using SaldoFlex.API.Infrastructure.Persistence;
using SaldoFlex.API.Shared.Models;

namespace SaldoFlex.API.Features.FinancialPlans.GetAll;

public record GetAllFinancialPlansQuery(
    int Page,
    int PageSize,
    string? Name
) : IRequest<Result<PaginatedResult<GetAllFinancialPlansResponse>>>;


public class GetAllFinancialPlansQueryHandler : IRequestHandler<GetAllFinancialPlansQuery, Result<PaginatedResult<GetAllFinancialPlansResponse>>>
{
    private readonly ApplicationDbContext _context;

    public GetAllFinancialPlansQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PaginatedResult<GetAllFinancialPlansResponse>>> Handle(GetAllFinancialPlansQuery request, CancellationToken cancellationToken)
    {
        var financialPlans = await _context.FinancialPlans
            .Where(x => request.Name == null || x.Name.Contains(request.Name))
            .Select(x => new GetAllFinancialPlansResponse(x.Id, x.Name, x.Description, x.CreatedAt, x.UpdatedAt))
            .ToPaginatedResultAsync(request.Page, request.PageSize, cancellationToken);

        return Result.Success(financialPlans);
    }
}