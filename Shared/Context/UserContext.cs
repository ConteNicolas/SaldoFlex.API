namespace SaldoFlex.API.Shared.Context;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccesor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccesor = httpContextAccessor;
    }

    public Guid? GetUserId()
    {
        var userId = _httpContextAccesor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == "UserId")?.Value;

        return userId is null ? null : Guid.Parse(userId);
    }
}
