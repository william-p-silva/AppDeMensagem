using AppDeMensagem.Core.Configuration;
using AppDeMensagem.Core.Http;
using AppDeMensagem.Core.Services;
using AppDeMensagem.Feature.Auth.Services;
using AppDeMensagem.Shared.Header.Services;
using AppDeMensagem.Web;
using AppDeMensagem.Web.Configuration;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddTransient<CookieHandler>();
builder.Services.AddHttpClient<HttpService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7140/");
}).AddHttpMessageHandler<CookieHandler>();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<Authentication>();

builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<CadastroService>();
builder.Services.AddScoped<UserSessionService>();
builder.Services.AddScoped<HeaderService>();

await builder.Build().RunAsync();
