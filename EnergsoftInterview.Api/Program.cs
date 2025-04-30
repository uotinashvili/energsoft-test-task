using EnergsoftInterview.Api.Common;
using EnergsoftInterview.Api.Common.DataContext;
using EnergsoftInterview.Api.Common.CosmosDb.Configuration;
using EnergsoftInterview.Api.Middleware;
using EnergsoftInterview.Api.Repositories;
using EnergsoftInterview.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

var sqlConnectionString = Environment.GetEnvironmentVariable("DefaultConnection");

if (string.IsNullOrWhiteSpace(sqlConnectionString))
{
    throw new Exception("Connection string not found in environment variables.");
}

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        sqlConnectionString,
        sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null
            );
        }
    ));

builder.Services.Configure<CosmosDbSettings>(options =>
{
    var connectionString = Environment.GetEnvironmentVariable("COSMOSDB_CONNECTIONSTRING");
    var databaseName = Environment.GetEnvironmentVariable("COSMOSDB_DATABASE");
    var containerName = Environment.GetEnvironmentVariable("COSMOSDB_CONTAINER");

    options.ConnectionString = connectionString;
    options.DatabaseName = databaseName;
    options.ContainerName = containerName;
});

builder.Services.AddScoped<SqlMeasurementRepository>();
builder.Services.AddScoped<CosmosMeasurementRepository>();
builder.Services.AddScoped<IMeasurementRepositoryFactory, MeasurementRepositoryFactory>();
builder.Services.AddScoped<IMeasurementService, MeasurementService>();
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<ITenantContext, TenantContext>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    if (context.Request.Headers.TryGetValue("X-Tenant-Token", out var token))
    {
        context.Items["TenantToken"] = token.ToString();
    }

    await next();
});

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
