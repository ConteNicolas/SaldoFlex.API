using FastEndpoints;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SaldoFlex.API.Domain;
using SaldoFlex.API.Infrastructure.Persistence;
using SaldoFlex.API.Shared.Models;

namespace SaldoFlex.API.Features.Currencies.Create;

public record CreateCurrencyCommand(
   string Symbol,
   string Code,
   string Description,
   bool IsDefault
) : IRequest<Result<CreateCurrencyResponse>>;


public class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, Result<CreateCurrencyResponse>>
{
    private ApplicationDbContext _context;

    public CreateCurrencyCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<CreateCurrencyResponse>> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
    {
        var exists = await _context.Currencies.AnyAsync(x => x.Symbol == request.Symbol && x.Code == request.Code && x.Description == request.Description, cancellationToken);

        if (exists)
        {
            return Result.Failure<CreateCurrencyResponse>(new Error("Currency.Create.Exists", "Currency already exists."));
        }

        var currency = new Currency()
        {
            Id = Guid.NewGuid(),
            Symbol = request.Symbol,
            Code = request.Code,
            Description = request.Description,
            IsDefault = request.IsDefault,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _context.Currencies.AddAsync(currency, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(MapToResponse(currency));
    }


    private CreateCurrencyResponse MapToResponse(Currency currency)
    {
        return new CreateCurrencyResponse(currency.Id, currency.Code, currency.Symbol, currency.Description, currency.IsDefault);
    }
}
