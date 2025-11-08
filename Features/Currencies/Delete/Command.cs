using MediatR;
using Microsoft.EntityFrameworkCore;
using SaldoFlex.API.Infrastructure.Persistence;
using SaldoFlex.API.Shared.Models;

namespace SaldoFlex.API.Features.Currencies.Delete;

public record DeleteCurrencyCommand(Guid Id) : IRequest<Result>;

public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand, Result>
{
    private readonly ApplicationDbContext _context;

    public DeleteCurrencyCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
    {
        var currency = await _context.Currencies.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (currency is null)
        {
            return Result.Failure<Result>(new Error("Currency.Delete.NotFound", "Currency not found."));
        }

        _context.Currencies.Remove(currency);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
