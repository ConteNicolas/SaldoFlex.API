
using FastEndpoints;
using Namotion.Reflection;
using SaldoFlex.API.Shared.Attributes;
using SaldoFlex.API.Shared.Models;
using SaldoFlex.API.Shared.Services;

namespace SaldoFlex.API.Shared.Middlewares.Filters;

public class ResourceOwnershipFilter : IEndpointFilter
{
    private readonly IResourceOwnershipService _ownershipService;

    public ResourceOwnershipFilter(IResourceOwnershipService ownershipService)
    {
        _ownershipService = ownershipService;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var endpoint = context.HttpContext.GetEndpoint();
        var endpointDef = endpoint?.Metadata.GetMetadata<EndpointDefinition>();

        var attr = endpointDef?.EndpointAttributes?.FirstOrDefault(x => x.GetType() == typeof(ResourceOwnershipRequiredAttribute));

        if (attr is null)
        {
            return await next(context);
        }

        var attrValues = (ResourceOwnershipRequiredAttribute)attr;

        var routeValue = context.HttpContext.Request.RouteValues[attrValues.RouteValue]?.ToString() ?? string.Empty;

        if (routeValue is null || !Guid.TryParse(routeValue, out Guid resourceId))
        {
            return Results.BadRequest(new Error("ResourceOwnershipFilter.Middleware.Filter", "Invalid resource id"));
        }

        var method = typeof(IResourceOwnershipService)
            .GetMethod("ValidateOwnership")
            .MakeGenericMethod(attrValues.EntityType);

        var result = await (Task<Result>)method.Invoke(_ownershipService, new object[] { resourceId })!;

        if (result.IsFailure)
        {
            return Results.BadRequest(result.Error);
        }


        return await next(context);
    }
}
