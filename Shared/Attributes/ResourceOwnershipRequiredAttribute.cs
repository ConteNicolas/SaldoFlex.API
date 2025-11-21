namespace SaldoFlex.API.Shared.Attributes;

public class ResourceOwnershipRequiredAttribute : Attribute
{
    public Type EntityType { get; set; }
    public string RouteValue { get; set; }

    public ResourceOwnershipRequiredAttribute(Type entityType, string routeValue)
    {
        EntityType = entityType;
        RouteValue = routeValue;
    }
}
