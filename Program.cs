using FinanceControl.Endpoints;
using FinanceControl.Database;
using FinanceControl.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", info: new OpenApiInfo {
        Title = "Api com Docker",
        Description = $"Api de Teste com docker compose, postgres and redis cache <strong>({builder.Environment.EnvironmentName})</strong>"
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("Database"))
);

builder.Services.AddStackExchangeRedisCache(options =>
    options.Configuration = builder.Configuration.GetConnectionString("Cache"));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.ApplyMigrations();

app.UseHttpsRedirection();

app.MapWeatherEndpoints();

app.Run();
