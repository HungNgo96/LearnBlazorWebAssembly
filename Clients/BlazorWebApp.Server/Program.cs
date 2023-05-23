using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
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
app.MapGet("/", context =>
{
    context.Response.Redirect("/ll");
    return Task.FromResult(0);
});

app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/ll"), appSub =>
    {
        //second.Use((ctx, nxt) =>
        //{
        //    ctx.Request.Path = "/ll" + ctx.Request.Path;
        //    return nxt();
        //});

        appSub.UseBlazorFrameworkFiles("/ll");
        appSub.UseStaticFiles();
        appSub.UseStaticFiles("/ll");
        appSub.UseRouting();

        appSub.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapFallbackToFile("/ll/{*path:nonfile}","ll/index.html");
        });
    });

app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/mud"), appSub =>
{
    //second.Use((ctx, nxt) =>
    //{
    //    ctx.Request.Path = "/ll" + ctx.Request.Path;
    //    return nxt();
    //});

    appSub.UseBlazorFrameworkFiles("/mud");
    appSub.UseStaticFiles();
    appSub.UseStaticFiles("/mud");
    appSub.UseRouting();

    appSub.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapFallbackToFile("/mud/{*path:nonfile}", "mud/index.html");
    });
});

await app.RunAsync();
