using SaldoFlex.API.Domain.Abstractions;
using SaldoFlex.API.Domain.Abstractions.Interfaces;

namespace SaldoFlex.API.Domain;

public class Tag : BaseEntity, IUserOwnedEntity
{
    public string Name { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}
