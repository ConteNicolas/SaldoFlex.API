using MediatR;
using SaldoFlex.API.Domain;
using SaldoFlex.API.Infrastructure.Extension;
using SaldoFlex.API.Infrastructure.Persistence;
using SaldoFlex.API.Shared.Models;

namespace SaldoFlex.API.Features.Currencies.GetAll;

public record GetAllCurrenciesQuery(
    string? Code = null,
    string? Description = null,
    string? Symbol = null,
    int Page = 1,
    int PageSize = 10
) : IRequest<Result<PaginatedResult<GetAllCurrenciesResponse>>>;



public class GetAllCurrenciesQueryHandler : IRequestHandler<GetAllCurrenciesQuery, Result<PaginatedResult<GetAllCurrenciesResponse>>>
{
    private readonly ApplicationDbContext _context;

    public GetAllCurrenciesQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PaginatedResult<GetAllCurrenciesResponse>>> Handle(GetAllCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var currencies = await _context.Currencies
            .Where(x => request.Code == null || x.Code == request.Code)
            .Where(x => request.Description == null || x.Description == request.Description)
            .Where(x => request.Symbol == null || x.Symbol == request.Symbol)
            .Select(x => MapToResponse(x))
            .ToPaginatedResultAsync(request.Page, request.PageSize, cancellationToken);

        return Result.Success(currencies);
    }

    private GetAllCurrenciesResponse MapToResponse(Currency currency)
    {
        return new GetAllCurrenciesResponse(currency.Id, currency.Code, currency.Symbol, currency.Description);
    }
}
