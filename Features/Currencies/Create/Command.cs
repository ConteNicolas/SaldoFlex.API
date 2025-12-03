using FastEndpoints;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SaldoFlex.API.Domain;
using SaldoFlex.API.Infrastructure.Persistence;
using SaldoFlex.API.Shared.Context;
using SaldoFlex.API.Shared.Models;

namespace SaldoFlex.API.Features.Currencies.Create;

public record CreateCurrencyCommand(
   string Symbol,
   string Code,
   string Description
) : IRequest<Result<CreateCurrencyResponse>>;


public class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, Result<CreateCurrencyResponse>>
{
    private ApplicationDbContext _context;
    private IUserContext _userContext;

    public CreateCurrencyCommandHandler(ApplicationDbContext context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<Result<CreateCurrencyResponse>> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
    {
        var exists = await _context.Currencies.AnyAsync(x => x.Symbol == request.Symbol && x.Code == request.Code && x.Description == request.Description, cancellationToken);

        if (exists)
        {
            return Result.Failure<CreateCurrencyResponse>(new Error("Currency.Create.Exists", "Currency already exists."));
        }

        var userId = _userContext.GetUserId().GetValueOrDefault();

        var currency = new Currency()
        {
            Id = Guid.NewGuid(),
            Symbol = request.Symbol,
            Code = request.Code,
            Description = request.Description,
            UserId = userId,
            IsDefault = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _context.Currencies.AddAsync(currency, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(MapToResponse(currency));
    }


    private CreateCurrencyResponse MapToResponse(Currency currency)
    {
        return new CreateCurrencyResponse(currency.Id, currency.Symbol, currency.Description, currency.Code, currency.CreatedAt, currency.UpdatedAt);
    }
}
