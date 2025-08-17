using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using TelecomCRM.WebServer;
using TelecomCRM.WebServer.ApiClients;
using TelecomCRM.WebServer.ApiClients.Interfaces;
using TelecomCRM.WebServer.Services;


var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы Razor и Blazor Server
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Здесь указываем адрес API сервера (например, localhost)
builder.Services.AddScoped<HttpClient>(sp =>
    new HttpClient { BaseAddress = new Uri("http://localhost:5144/") });


// Регистрируем свои сервисы (например, API клиенты)
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();

builder.Services.AddScoped<ICustomerApiClient, CustomerApiClient>();
builder.Services.AddScoped<AuthApiClient>();
builder.Services.AddScoped<ServiceApiClient>();
builder.Services.AddScoped<SubscriptionApiClient>();

builder.Services.AddBlazoredLocalStorage();


var app = builder.Build();

// Настройка пайплайна
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
