using Microsoft.EntityFrameworkCore;
using Softqu.API.Extensions;
using Softqu.Infrastructure.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Services Configuration ---
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScalarConfiguration(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("softqu-db-secret")));

builder.Services.AddDistributedMemoryCache();

var app = builder.Build();

// --- 2. HTTP Request Pipeline (Middleware) ---
await app.UseDatabaseConfiguration();
app.UseApiDocumentation();
app.UseApplicationMiddleware();

app.Run();