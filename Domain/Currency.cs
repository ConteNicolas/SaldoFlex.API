using SaldoFlex.API.Domain.Abstractions;
using SaldoFlex.API.Domain.Abstractions.Interfaces;

namespace SaldoFlex.API.Domain;

public class Currency : BaseEntity, IUserOwnedEntity
{
    public string Description { get; set; }
    public string Code { get; set; }
    public string Symbol { get; set; }
    public bool IsDefault { get; set; } = false;

    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}
