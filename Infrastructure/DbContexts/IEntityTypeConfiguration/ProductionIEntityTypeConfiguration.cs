using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DbContexts.IEntityTypeConfiguration
{
    public class ProductionIEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            _ = builder.ToTable("Product", schema: "BlazorWebAssemly");
            _ = builder.HasKey(t => t.Id);
            _ = builder.Property(t => t.Id).HasColumnName("Id");
            _ = builder.Property(t => t.Name).HasColumnName("Name");
            _ = builder.Property(t => t.Supplier).HasColumnName("Supplier");
            _ = builder.Property(t => t.Price).HasColumnName("Price");
            _ = builder.Property(t => t.ImageUrl).HasColumnName("ImageUrl");
        }
    }
}
