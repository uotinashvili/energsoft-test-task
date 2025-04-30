using EnergsoftInterview.Api.Common;
using EnergsoftInterview.Api.Common.DataContext;
using EnergsoftInterview.Api.Common.CosmosDb.Configuration;
using EnergsoftInterview.Api.Middleware;
using EnergsoftInterview.Api.Repositories;
using EnergsoftInterview.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

// Configure JWT Authentication
var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? throw new Exception("JWT Secret is not configured");
var key = Encoding.ASCII.GetBytes(jwtSecret);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddScoped<SqlMeasurementRepository>();
builder.Services.AddScoped<CosmosMeasurementRepository>();
builder.Services.AddScoped<IMeasurementRepositoryFactory, MeasurementRepositoryFactory>();
builder.Services.AddScoped<IMeasurementService, MeasurementService>();
builder.Services.AddScoped<ICustomerContext, CustomerContext>();
builder.Services.AddScoped<JwtAuthenticationService>();
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

app.UseAuthentication();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
