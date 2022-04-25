using Blazored.LocalStorage;
using Indego;
using Indego.Client;
using Indego.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Refit;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddI18nText();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthStorage, AuthStorage>();
builder.Services.AddSingleton<GlobalState>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IIndegoService, IndegoService>();

builder.Services.AddMudServices();

builder.Services.AddRefitClient<IIndegoClient>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://api.indego.iot.bosch-si.com/api/v1");
            });

await builder.Build().RunAsync();
