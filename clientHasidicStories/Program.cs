using clientHasidicStories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Globalization;
using Microsoft.JSInterop;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Build the configuration
var configuration = builder.Configuration;

// Add services to the service collection
builder.Services
    .AddScoped(sp =>
        new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
    .AddLocalization()
    .AddBlazorise(options =>
         {
             options.Immediate = true;
         })
    .AddBootstrap5Providers()
    .AddFontAwesomeIcons();

string baseAddress = builder.Configuration.GetValue<string>("endpointURL");
builder.Services.AddScoped(sp => 
        new HttpClient { BaseAddress = new Uri(baseAddress) });

var host = builder.Build();

const string defaultCulture = "he-IL";
var js = host.Services.GetRequiredService<IJSRuntime>();
var result = await js.InvokeAsync<string>("blazorCulture.get");
var culture = CultureInfo.GetCultureInfo(result ?? defaultCulture);
if (result == null)
{
    await js.InvokeVoidAsync("blazorCulture.set", defaultCulture);
}
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await host.RunAsync();