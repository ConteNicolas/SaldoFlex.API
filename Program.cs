global using FluentValidation;

using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using SaldoFlex.API.Infrastructure.Persistence;
using SaldoFlex.API.Shared.Context;
using SaldoFlex.API.Shared.Middlewares.Filters;
using SaldoFlex.API.Shared.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.Configure<JwtCreationOptions>(opt =>
{
    opt.SigningKey = builder.Configuration["JWT:Key"];
    opt.Issuer = builder.Configuration["JWT:Issuer"];
    opt.Audience = builder.Configuration["JWT:Audience"];
});

builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(Program).Assembly));

builder.Services
    .AddAuthenticationJwtBearer(b => b.SigningKey = builder.Configuration["JWT:Key"])
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

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<IResourceOwnershipService, ResourceOwnershipService>();
builder.Services.AddTransient<IUserContext, UserContext>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints(opt =>
    {
        opt.Endpoints.RoutePrefix = "api";
        opt.Endpoints.Configurator = ep => ep.Options(o => o.AddEndpointFilter<ResourceOwnershipFilter>());
    })
    .UseSwaggerGen();

app.Run();
