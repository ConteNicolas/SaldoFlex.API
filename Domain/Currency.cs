using SaldoFlex.API.Domain.Abstractions;

namespace SaldoFlex.API.Domain;

public class Currency : BaseEntity
{
    public string Description { get; set; }
    public string Code { get; set; }
    public string Symbol { get; set; }
    public bool IsDefault { get; set; } = false;
}
