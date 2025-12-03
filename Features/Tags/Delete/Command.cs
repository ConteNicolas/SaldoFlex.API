using MediatR;
using Microsoft.EntityFrameworkCore;
using SaldoFlex.API.Infrastructure.Persistence;
using SaldoFlex.API.Shared.Models;

namespace SaldoFlex.API.Features.Tags.Delete;

public record DeleteTagCommand(Guid Id) : IRequest<Result>;

public record class DeleteTagCommandHandler(ApplicationDbContext context) : IRequestHandler<DeleteTagCommand, Result>
{
    public async Task<Result> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await context.Tags.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
          
        if (tag is null)
        {
            return Result.Failure<Result>(new Error("Tag.Delete.NotFound", "Tag not found."));
        }

        if (await context.FinancialSceneExpenses.AnyAsync(x => x.Tags.Any(t => t.Id == tag.Id), cancellationToken))
        {
            return Result.Failure<Result>(new Error("Tag.Delete.InUse", "Tag is in use."));
        }

        context.Tags.Remove(tag);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
