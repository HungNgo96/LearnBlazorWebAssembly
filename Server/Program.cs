using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

// This worked in .NET 6
//app.UseBlazorFrameworkFiles("/myblazorapp");

// This doesn't work either
//app.UseBlazorFrameworkFiles("/myroot/myblazorapp");
//app.UseStaticFiles();

// And finally, the approach from 
// https://learn.microsoft.com/en-us/aspnet/core/blazor/host-and-deploy/multiple-hosted-webassembly?view=aspnetcore-7.0
// doesn't work either.
//app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/ll"), first =>
//{
//    first.UseBlazorFrameworkFiles("/ll");
//    first.UseStaticFiles();
//    first.UseStaticFiles("/ll");
//    first.UseRouting();
//    first.UseEndpoints(endpoints =>
//    {
//        endpoints.MapControllers();
//        endpoints.MapFallbackToFile("ll/{*path:nonfile}",
//            "ll/index.html");
//    });
//});

app.UseBlazorFrameworkFiles("/ll");
app.UseStaticFiles();
app.UseStaticFiles("/ll");
app.UseRouting();
app.MapControllers();
app.MapFallbackToFile("/ll/{*path:nonfile}","ll/index.html");

await app.RunAsync();
