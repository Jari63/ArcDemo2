using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ArcDemo2.Web.Domain.ProductAggregate;

namespace ArcDemo2.Web.Infrastructure.Data.Config;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
  public void Configure(EntityTypeBuilder<Product> builder)
  {
    builder.Property(entity => entity.Id)
      .HasValueGenerator<VogenIntIdValueGenerator<AppDbContext, Product, ProductId>>()
      .HasVogenConversion()
      .IsRequired();

    builder.Property(entity => entity.Name)
      .HasMaxLength(100)
      .IsRequired();

    builder.Property(entity => entity.UnitPrice)
      .HasPrecision(18, 2)
      .IsRequired();

    builder.HasData(
      Product.Create(
        "Coffee Mug",
        9.99m),
      Product.Create(
        "T-Shirt",
        19.99m),
      Product.Create(
        "Sticker Pack",
        3.99m)
    );
  }
}
