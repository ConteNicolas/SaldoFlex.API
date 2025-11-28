using FastEndpoints;
using MediatR;

namespace SaldoFlex.API.Features.Auth.SignIn;

public class SignInEndpoint : Endpoint<SignInRequest>
{
    private readonly ISender _sender;

    public SignInEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post("/auth/sign-in");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SignInRequest req, CancellationToken ct)
    {
        var command = MapToCommand(req);
        var result = await _sender.Send(command, ct);

        if (result.IsFailure)
        {
            await Send.ResponseAsync(result.Error, StatusCodes.Status400BadRequest, ct);
            return;
        }

        await Send.OkAsync(result.Value, ct);
    }
    
    private SignInCommand MapToCommand(SignInRequest req)
    {
        return new SignInCommand(req.Username, req.Password);
    }
}
