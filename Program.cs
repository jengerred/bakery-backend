using BakeryBackend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------
// SERVICES
// ---------------------------------------------------------

// ✔ .NET 8 Swagger (replaces AddOpenApi)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

// ✔ Database
builder.Services.AddDbContext<BakeryContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// ✔ CORS POLICY
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// ---------------------------------------------------------
// MIDDLEWARE PIPELINE
// ---------------------------------------------------------

if (app.Environment.IsDevelopment())
{
    // ✔ .NET 8 Swagger UI (replaces MapOpenApi)
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✔ Apply CORS before controllers
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
