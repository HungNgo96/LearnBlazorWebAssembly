using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Options;
using Application.Ultilities;
using Infrastructure.DbContexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Application.Extensions;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddCors(policy =>
{
    policy.AddPolicy("CorsPolicy", opt => opt
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
_ = builder.Services.Configure<ConnectionStringOptions>(builder.Configuration.GetRequiredSection("ConnectionStrings"));
services.AddControllers();

services.AddDbContext<BlazorWebContext>(op =>
{
    op.UseSqlServer(config.GetRequiredSection("ConnectionStrings:SqlServer").Value, x => x.MigrationsAssembly("Infrastructure"));
});
services.AddApplicationLayer();
services.AddScoped(typeof(Lazy<>), typeof(LazyInstanceUtils<>));
services.AddScoped<IProductRepository, ProductRepository>();
services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
