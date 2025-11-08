using SaldoFlex.API.Domain.Abstractions;

namespace SaldoFlex.API.Domain;

public class FinancialScene : BaseEntity
{
    public string? Description { get; set; }

    public Guid FinancialPlanId { get; set; }
    public virtual FinancialPlan FinancialPlan { get; set; }

    public virtual ICollection<FinancialSceneGroup> Groups { get; set; } = new List<FinancialSceneGroup>();
}
