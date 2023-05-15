using ApplicationClient.Interfaces;
using ApplicationClient.Services;
using ApplicationClient.Ultilities;
using BlazorWebApp;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Newtonsoft.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(typeof(Lazy<>), typeof(LazyInstanceUtils<>));

IConfiguration config = builder.Configuration;
Console.WriteLine("config::" + JsonConvert.SerializeObject(config));

//add httpClient
builder.Services.AddHttpClient();
var urlApi = config.GetRequiredSection("Api:Url").Value ?? builder.HostEnvironment.BaseAddress;
Console.WriteLine("urlApi::" + urlApi);
builder.Services.AddHttpClient("ProductsAPI", (cl) => cl.BaseAddress = new Uri(urlApi));
builder.Services.AddScoped<IProductService, ProductService>();
//override httpclient default
//builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ProductsAPI"));

var app = builder.Build();

await app.RunAsync();
