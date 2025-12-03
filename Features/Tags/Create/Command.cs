using MediatR;
using Microsoft.EntityFrameworkCore;
using SaldoFlex.API.Domain;
using SaldoFlex.API.Infrastructure.Persistence;
using SaldoFlex.API.Shared.Context;
using SaldoFlex.API.Shared.Models;

namespace SaldoFlex.API.Features.Tags.Create;

public record CreateTagCommand(
    string Name    
) : IRequest<Result<CreateTagResponse>>;

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Result<CreateTagResponse>>
{
    private readonly ApplicationDbContext _context;
    private IUserContext _userContext;

    public CreateTagCommandHandler(ApplicationDbContext context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<Result<CreateTagResponse>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var exists = await _context.Tags.AnyAsync(x => x.Name == request.Name, cancellationToken);

        if (exists) {
            return Result.Failure<CreateTagResponse>(new Error("Tag.Create.Exists", "Tag already exists."));
        }

        var userId = _userContext.GetUserId().GetValueOrDefault();

        var tag = new Tag
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            CreatedAt = DateTime.UtcNow,
            UserId = userId,
        };

        await _context.Tags.AddAsync(tag, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(MapToResponse(tag));
    }

    private CreateTagResponse MapToResponse(Tag tag)
    {
        return new CreateTagResponse(tag.Id, tag.Name, tag.CreatedAt, tag.UpdatedAt);
    }
}
