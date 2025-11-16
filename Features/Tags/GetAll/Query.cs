using MediatR;
using SaldoFlex.API.Infrastructure.Extension;
using SaldoFlex.API.Infrastructure.Persistence;
using SaldoFlex.API.Shared.Models;

namespace SaldoFlex.API.Features.Tags.GetAll;

public record GetAllTagsQuery(
    int Page = 1,
    int PageSize = 10,
    string? Name = null
) : IRequest<Result<PaginatedResult<GetAllTagsResponse>>>;

public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, Result<PaginatedResult<GetAllTagsResponse>>>
{
    private readonly ApplicationDbContext _context;

    public GetAllTagsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PaginatedResult<GetAllTagsResponse>>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var tags = await _context.Tags
            .Where(x => request.Name == null || x.Name.Contains(request.Name))
            .Select(x => new GetAllTagsResponse(x.Id, x.Name))
            .ToPaginatedResultAsync(request.Page, request.PageSize, cancellationToken);

        return Result.Success(tags);
    }
}
