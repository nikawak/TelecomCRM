using Microsoft.EntityFrameworkCore;
using TelecomCRM.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Add DbContext
builder.Services.AddDbContext<TelecomDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Add controllers (или minimal API)
builder.Services.AddControllers();

// 🔹 Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔹 Enable Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🔹 Middleware
app.UseHttpsRedirection();
app.UseAuthorization();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.MapFallbackToFile("index.html");

// 🔹 Routing
app.MapControllers();

app.Run();
