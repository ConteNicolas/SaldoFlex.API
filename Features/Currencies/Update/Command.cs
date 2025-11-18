using MediatR;
using Microsoft.EntityFrameworkCore;
using SaldoFlex.API.Domain;
using SaldoFlex.API.Infrastructure.Persistence;
using SaldoFlex.API.Shared.Models;

namespace SaldoFlex.API.Features.Currencies.Update;

public record UpdateCurrencyCommand(
    Guid Id,
    string? Symbol,
    string? Code,
    string? Description,
    bool? IsDefault
) : IRequest<Result<UpdateCurrencyResponse>>;


public class UpdateCurrencyCommandHandler : IRequestHandler<UpdateCurrencyCommand, Result<UpdateCurrencyResponse>>
{
    private readonly ApplicationDbContext _context;

    public UpdateCurrencyCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<UpdateCurrencyResponse>> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
    {
        var currency = await _context.Currencies.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (currency is null)
        {
            return Result.Failure<UpdateCurrencyResponse>(new Error("Currency.Update.NotFound", "Currency not found."));
        }

        currency.Description = request.Description ?? currency.Description;
        currency.Code = request.Code ?? currency.Code;
        currency.Symbol = request.Symbol ?? currency.Symbol;
        currency.IsDefault = request?.IsDefault ?? currency.IsDefault;

        currency.UpdatedAt = DateTime.UtcNow;

        _context.Currencies.Update(currency);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(MapToResponse(currency));
    }

    private UpdateCurrencyResponse MapToResponse(Currency currency)
    {
        return new UpdateCurrencyResponse(currency.Id, currency.Code, currency.Symbol, currency.Description, currency.IsDefault);
    }
}
