using SaldoFlex.API.Domain.Abstractions;

namespace SaldoFlex.API.Domain;

public class User : BaseEntity
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string? Email { get; set; }
    public DateTime? LastLogin { get; set; }
}
