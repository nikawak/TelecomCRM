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

// ����� ��������� ����� API ������� (��������, localhost)
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001/") });

// ������������ ���� ������� (��������, API �������)
builder.Services.AddScoped<ICustomerApiClient, CustomerApiClientMock>();

await builder.Build().RunAsync();
