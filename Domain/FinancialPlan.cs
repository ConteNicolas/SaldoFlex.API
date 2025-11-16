using SaldoFlex.API.Domain.Abstractions;

namespace SaldoFlex.API.Domain;

public class FinancialPlan : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }

    public Guid FinancialSceneId { get; set; }
    public virtual FinancialScene FinancialScene { get; set; }
}
