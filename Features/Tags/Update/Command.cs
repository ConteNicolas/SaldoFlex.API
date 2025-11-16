using MediatR;
using Microsoft.EntityFrameworkCore;
using SaldoFlex.API.Domain;
using SaldoFlex.API.Infrastructure.Persistence;
using SaldoFlex.API.Shared.Models;

namespace SaldoFlex.API.Features.Tags.Update;

public record UpdateTagCommand(Guid Id, string Name) : IRequest<Result<UpdateTagResponse>>;

public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, Result<UpdateTagResponse>>
{
    private readonly ApplicationDbContext _context;

    public UpdateTagCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<UpdateTagResponse>> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var tag  = await _context.Tags.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (tag is null)
        {
            return Result.Failure<UpdateTagResponse>(new Error("Tag.Update.NotFound", "Tag not found."));
        }

        tag.Name = request.Name ?? tag.Name;

        tag.UpdatedAt = DateTime.UtcNow;

        _context.Tags.Update(tag);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(MapToResponse(tag));
    }

    private UpdateTagResponse MapToResponse(Tag tag)
    {
        return new UpdateTagResponse(tag.Id, tag.Name);
    }
}
