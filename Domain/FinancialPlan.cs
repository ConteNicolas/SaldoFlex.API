using SaldoFlex.API.Domain.Abstractions;
using SaldoFlex.API.Domain.Abstractions.Interfaces;

namespace SaldoFlex.API.Domain;

public class FinancialPlan : BaseEntity, IUserOwnedEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }

    public Guid FinancialSceneId { get; set; }
    public virtual FinancialScene FinancialScene { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}
