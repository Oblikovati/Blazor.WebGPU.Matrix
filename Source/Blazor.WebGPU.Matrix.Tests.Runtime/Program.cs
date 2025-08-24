using Blazor.WebGPU.Matrix.Tests.Runtime;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SpawnDev.BlazorJS;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazorJSRuntime();

builder.Services.Configure<JsonSerializerOptions>(options =>
{
    options.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
});

await builder.Build().BlazorJSRunAsync();
