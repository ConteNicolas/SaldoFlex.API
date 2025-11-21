using FastEndpoints;
using MediatR;
using SaldoFlex.API.Domain.Enums;

namespace SaldoFlex.API.Features.Users.GetMe;

public class GetMeEndpoint : EndpointWithoutRequest
{
    private readonly ISender _sender;

    public GetMeEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get("/users/me");
        Claims(nameof(ClaimsEnum.UserId));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var query = new GetMeQuery();
        var result = await _sender.Send(query, ct);

        if (result.IsFailure)
        {
            await Send.ResponseAsync(result.Error, StatusCodes.Status400BadRequest, ct);
            return;
        }

        await Send.OkAsync(result.Value, ct);
    }
}
