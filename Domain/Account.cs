using SaldoFlex.API.Domain.Abstractions;
using SaldoFlex.API.Domain.Abstractions.Interfaces;

namespace SaldoFlex.API.Domain;

public class Account : BaseEntity, IUserOwnedEntity
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}
