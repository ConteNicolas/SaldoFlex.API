using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using SaldoFlex.API.Infrastructure.Persistence;
using SaldoFlex.API.Shared.Context;
using SaldoFlex.API.Shared.Middlewares.Filters;
using SaldoFlex.API.Shared.Services;

namespace SaldoFlex.API.Infrastructure;

public static class DependencyInjection
{

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }

    public static IServiceCollection AddFastEndpointWithJWTAndSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtCreationOptions>(opt =>
        {
            opt.SigningKey = configuration["JWT:Key"];
            opt.Issuer = configuration["JWT:Issuer"];
            opt.Audience = configuration["JWT:Audience"];
        });

        services
            .AddAuthenticationJwtBearer(b => b.SigningKey = configuration["JWT:Key"])
            .AddAuthorization()
            .AddFastEndpoints()
            .SwaggerDocument(opt =>
            {
                opt.DocumentSettings = b =>
                {
                    b.Title = "Saldo Flex";
                    b.Description = "Saldo flex es una plataforma que te permite gestionar de forma eficiente tu dinero. El como lo seleccionas vos a traves de nuestro sistema flexible.";
                    b.Version = "v1";
                };
            });

        return services;
    }

    public static IServiceCollection AddLibraries(this IServiceCollection services)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(Program).Assembly));
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IResourceOwnershipService, ResourceOwnershipService>();
        services.AddTransient<IUserContext, UserContext>();

        return services;
    }
}
