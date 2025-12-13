using MediatR;
using SaldoFlex.API.Infrastructure.Extension;
using SaldoFlex.API.Infrastructure.Persistence;
using SaldoFlex.API.Shared.Context;
using SaldoFlex.API.Shared.Enums;
using SaldoFlex.API.Shared.Models;

namespace SaldoFlex.API.Features.Tags.GetAll;

public record GetAllTagsQuery(
    int Page,
    int PageSize,
    string? Name,
    DateFilterTypes DateFilter,
    OrderByTypes OrderBy,
    OrderDirectionTypes OrderDirection
) : IRequest<Result<PaginatedResult<GetAllTagsResponse>>>;

public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, Result<PaginatedResult<GetAllTagsResponse>>>
{
    private readonly ApplicationDbContext _context;
    private IUserContext _userContext;

    public GetAllTagsQueryHandler(ApplicationDbContext context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<Result<PaginatedResult<GetAllTagsResponse>>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId().GetValueOrDefault();

        var tags = await _context.Tags
            .Where(x => request.Name == null || x.Name.Contains(request.Name))
            .Where(x => x.UserId == userId)
            .ApplyDateFilter(request.DateFilter)
            .ApplyOrderBy(request.OrderBy, request.OrderDirection)
            .Select(x => new GetAllTagsResponse(x.Id, x.Name, x.CreatedAt, x.UpdatedAt))
            .ToPaginatedResultAsync(request.Page, request.PageSize, cancellationToken);

        return Result.Success(tags);
    }
}
