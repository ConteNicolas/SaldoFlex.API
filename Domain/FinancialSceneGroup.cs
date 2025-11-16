using SaldoFlex.API.Domain.Abstractions;

namespace SaldoFlex.API.Domain;

public class FinancialSceneGroup : BaseEntity
{
    public string Name { get; set; }

    public Guid FinancialSceneId { get; set; }
    public virtual FinancialScene FinancialScene { get; set; }

    public virtual ICollection<FinancialSceneExpense> Expenses { get; set; } = new List<FinancialSceneExpense>();
}
