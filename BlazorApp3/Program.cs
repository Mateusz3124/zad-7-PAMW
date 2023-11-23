using BlazorApp3;
using Configuration;
using Services.BookServices;
using Microsoft.Extensions.DependencyInjection;
using P06.Shared.Services.BookService;
using P06.Shared.Services.ProductService;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var appSettings = builder.Configuration.GetSection(nameof(AppSettings));
var appSettingsSection = appSettings.Get<AppSettings>();

var uriBuilder = new UriBuilder(appSettingsSection.BaseAPIUrl)
{
    Path = appSettingsSection.BaseBookEndpoint.Base_url,
};
//Microsoft.Extensions.Http
builder.Services.AddHttpClient<IBookService, BookService>(client => client.BaseAddress = uriBuilder.Uri);

builder.Services.AddSingleton<IOptions<AppSettings>>(new OptionsWrapper<AppSettings>(appSettingsSection));


await builder.Build().RunAsync();
