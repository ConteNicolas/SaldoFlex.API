using SaldoFlex.API.Domain.Abstractions;
using SaldoFlex.API.Domain.Abstractions.Interfaces;

namespace SaldoFlex.API.Domain;

public enum FinancialSceneExpenseStateEnum
{
    Unpaid,
    Paid
}

public class FinancialSceneExpense : BaseEntity, IUserOwnedEntity
{
    public string Name { get; set; }

    public Guid GroupId { get; set; }
    public virtual FinancialSceneGroup Group { get; set; }

    public Guid CurrencyId { get; set; }
    public virtual Currency Currency { get; set; }

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public double Amount { get; set; }
    public FinancialSceneExpenseStateEnum State { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}
