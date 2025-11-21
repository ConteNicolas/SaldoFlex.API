namespace SaldoFlex.API.Domain.Abstractions.Interfaces;

public interface IUserOwnedEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }
}
