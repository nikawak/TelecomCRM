using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TelecomCRM.WebClient;
using TelecomCRM.WebClient.ApiClients;
using TelecomCRM.WebClient.ApiClients.Interfaces;
using TelecomCRM.WebClient.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// Здесь указываем адрес API сервера (например, localhost)
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5144/") });

builder.Services.AddBlazoredLocalStorage();

// Регистрируем свои сервисы (например, API клиенты)
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();

builder.Services.AddScoped<ICustomerApiClient, CustomerApiClient>();
builder.Services.AddScoped<AuthApiClient>();

await builder.Build().RunAsync();
