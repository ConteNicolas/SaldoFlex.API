using FastEndpoints;
using MediatR;

namespace SaldoFlex.API.Features.Auth.SignUp;

public class SignUpEndpoint : Endpoint<SignUpRequest>
{
    private readonly ISender _sender;

    public SignUpEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post("/auth/sign-up");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SignUpRequest req, CancellationToken ct)
    {
        var command = MapToCommand(req);
        var result = await _sender.Send(command, ct);

        if (result.IsFailure)
        {
            await Send.ResponseAsync(result.Error, StatusCodes.Status400BadRequest, ct);
            return;
        }

        await Send.ResponseAsync(string.Empty, StatusCodes.Status201Created, ct);
    }

    private SignUpCommand MapToCommand(SignUpRequest req)
    {
        return new SignUpCommand(req.Username, req.Password, req?.Email, req.Firstname, req.Lastname);
    }
}
