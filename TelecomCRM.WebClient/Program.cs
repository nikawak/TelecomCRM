using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TelecomCRM.WebClient;
using TelecomCRM.WebClient.ApiClients;
using TelecomCRM.WebClient.ApiClients.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// Здесь указываем адрес API сервера (например, localhost)
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5144/") });

// Регистрируем свои сервисы (например, API клиенты)
builder.Services.AddScoped<ICustomerApiClient, CustomerApiClient>();

await builder.Build().RunAsync();
