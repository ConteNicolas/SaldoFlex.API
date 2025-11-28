global using FluentValidation;

using FastEndpoints;
using FastEndpoints.Swagger;
using SaldoFlex.API.Infrastructure;
using SaldoFlex.API.Shared.Middlewares.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
    });
});

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddFastEndpointWithJWTAndSwagger(builder.Configuration);
builder.Services.AddLibraries();
builder.Services.AddServices();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseCors();

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
