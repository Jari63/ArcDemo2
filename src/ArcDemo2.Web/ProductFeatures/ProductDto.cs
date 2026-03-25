using ArcDemo2.Web.Domain.ProductAggregate;

namespace ArcDemo2.Web.ProductFeatures;
public record ProductDto(ProductId Id, string Name, decimal UnitPrice);
