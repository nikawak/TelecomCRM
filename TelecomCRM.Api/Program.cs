using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TelecomCRM.Application.Queries;
using TelecomCRM.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Add DbContext
builder.Services.AddDbContext<TelecomDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Add controllers (или minimal API)
builder.Services.AddControllers();

var assembly = AppDomain.CurrentDomain.Load("TelecomCRM.Application");
builder.Services.AddMediatR(cfg => 
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);              // Api проект
    cfg.RegisterServicesFromAssembly(typeof(GetAllCustomersQuery).Assembly);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});


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


app.UseCors("AllowAll");

// 🔹 Routing
app.MapControllers();

app.Run();
