using SaldoFlex.API.Domain.Abstractions;

namespace SaldoFlex.API.Domain;

public enum FinancialSceneExpenseStateEnum
{
    Unpaid,
    Paid
}

public class FinancialSceneExpense : BaseEntity
{
    public string Name { get; set; }

    public Guid GroupId { get; set; }
    public virtual FinancialSceneGroup Group { get; set; }

    public Guid CurrencyId { get; set; }
    public virtual Currency Currency { get; set; }

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public double Amount { get; set; }
    public FinancialSceneExpenseStateEnum State { get; set; }
}
