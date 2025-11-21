using SaldoFlex.API.Domain.Abstractions;
using SaldoFlex.API.Domain.Abstractions.Interfaces;

namespace SaldoFlex.API.Domain;

public class FinancialScene : BaseEntity, IUserOwnedEntity
{
    public Guid FinancialPlanId { get; set; }
    public virtual FinancialPlan FinancialPlan { get; set; }

    public virtual ICollection<FinancialSceneGroup> Groups { get; set; } = new List<FinancialSceneGroup>();
    public virtual ICollection<FinancialSceneIncome> Incomes { get; set; } = new List<FinancialSceneIncome>();

    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}
