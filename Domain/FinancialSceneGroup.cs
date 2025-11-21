using SaldoFlex.API.Domain.Abstractions;
using SaldoFlex.API.Domain.Abstractions.Interfaces;

namespace SaldoFlex.API.Domain;

public class FinancialSceneGroup : BaseEntity, IUserOwnedEntity
{
    public string Name { get; set; }

    public Guid FinancialSceneId { get; set; }
    public virtual FinancialScene FinancialScene { get; set; }

    public virtual ICollection<FinancialSceneExpense> Expenses { get; set; } = new List<FinancialSceneExpense>();


    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}
