using ApplicationClient.AuthProviders;
using ApplicationClient.Interfaces;
using ApplicationClient.Options;
using ApplicationClient.Services;
using ApplicationClient.Ultilities;
using Blazored.LocalStorage;
using BlazorWebApp;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { 
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
}
.EnableIntercept(sp));

builder.Services.AddScoped(typeof(Lazy<>), typeof(LazyInstanceUtils<>));
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddHttpClientInterceptor();
//builder.Services.AddScoped<AuthenticationStateProvider, TestAuthStateProvider>();//test 
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
IConfiguration config = builder.Configuration;

//add httpClient
builder.Services.AddHttpClient();
var urlApi = config.GetRequiredSection("Api:Url").Value ?? builder.HostEnvironment.BaseAddress;
_ = builder.Services.Configure<UrlOption>(config.GetRequiredSection("Api"));
builder.Services.AddHttpClient("ProductsAPI", (cl) => cl.BaseAddress = new Uri(urlApi));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<RefreshTokenService>();
builder.Services.AddScoped<HttpInterceptorService>();

//builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>((cl) => cl.BaseAddress = new Uri(urlApi));
//override httpclient default
//builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ProductsAPI"));

var app = builder.Build();

await app.RunAsync();
