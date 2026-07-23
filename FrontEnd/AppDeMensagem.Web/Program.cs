using AppDeMensagem.Core.Http;
using AppDeMensagem.Feature.Auth.Services;
using AppDeMensagem.Web;
using AppDeMensagem.Web.Configuration;
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

builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<CadastroService>();

await builder.Build().RunAsync();
