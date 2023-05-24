using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.DbContexts
{
    public class BlazorWebContext : IdentityDbContext<User>
    {
        public BlazorWebContext(DbContextOptions<BlazorWebContext> options)
         : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVirtual> ProductVirtuals { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}