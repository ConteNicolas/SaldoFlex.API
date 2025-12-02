using MediatR;
using SaldoFlex.API.Infrastructure.Extension;
using SaldoFlex.API.Infrastructure.Persistence;
using SaldoFlex.API.Shared.Context;
using SaldoFlex.API.Shared.Enums;
using SaldoFlex.API.Shared.Models;

namespace SaldoFlex.API.Features.FinancialPlans.GetAll;

public record GetAllFinancialPlansQuery(
    int Page,
    int PageSize,
    string? Name,
    DateFilterTypes DateFilter,
    OrderByTypes OrderBy,
    OrderDirectionTypes OrderDirection
) : IRequest<Result<PaginatedResult<GetAllFinancialPlansResponse>>>;


public class GetAllFinancialPlansQueryHandler : IRequestHandler<GetAllFinancialPlansQuery, Result<PaginatedResult<GetAllFinancialPlansResponse>>>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserContext _userContext;

    public GetAllFinancialPlansQueryHandler(ApplicationDbContext context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<Result<PaginatedResult<GetAllFinancialPlansResponse>>> Handle(GetAllFinancialPlansQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId().GetValueOrDefault();

        var financialPlans = await _context.FinancialPlans
            .Where(x => request.Name == null || x.Name.ToLower().Contains(request.Name.ToLower()))
            .Where(x => x.UserId == userId)
            .ApplyDateFilter(request.DateFilter)
            .ApplyOrderBy(request.OrderBy, request.OrderDirection)
            .Select(x => new GetAllFinancialPlansResponse(x.Id, x.Name, x.Description, x.CreatedAt, x.UpdatedAt, x.Tags.Select(t => new GetAllFinancialPlanTagsResponse(t.Id, t.Name)).ToList()))
            .ToPaginatedResultAsync(request.Page, request.PageSize, cancellationToken);

        return Result.Success(financialPlans);
    }
}