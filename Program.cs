global using FluentValidation;

using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using SaldoFlex.API.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

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
            b.Description = "Saldo flex es una plataforma que te permite gestionar de forma eficiente tu dinero. El como lo seleccionas vos a traves de nuestro sistema flexible."
            b.Version = "v1";
        };
    });

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseNpgsql();
});

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints()
    .UseSwaggerGen();

app.Run();
