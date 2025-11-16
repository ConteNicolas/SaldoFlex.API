using SaldoFlex.API.Domain.Abstractions;

namespace SaldoFlex.API.Domain;

public class FinancialSceneIncome : BaseEntity
{
    public string Name { get; set; }
    public double Amount { get; set; }
}
