using Microsoft.AspNetCore.Mvc;

namespace SaldoFlex.API.Features.Currencies.GetAll;

public record GetAllCurrenciesRequest(
    int Page = 1,
    int PageSize = 10,
    string? Code = null,
    string? Description = null,
    string? Symbol = null
);

public record GetAllCurrenciesResponse(
    Guid Id,
    string Code,
    string Symbol,
    string Description,
    bool IsDefault
);