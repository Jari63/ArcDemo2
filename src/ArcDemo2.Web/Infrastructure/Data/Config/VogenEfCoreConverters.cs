using ArcDemo2.Web.Domain.CartAggregate;
using ArcDemo2.Web.Domain.GuestUserAggregate;
using ArcDemo2.Web.Domain.OrderAggregate;
using ArcDemo2.Web.Domain.ProductAggregate;
using Vogen;

namespace ArcDemo2.Web.Infrastructure.Data.Config;

[EfCoreConverter<ProductId>]
[EfCoreConverter<CartId>]
[EfCoreConverter<CartItemId>]
[EfCoreConverter<GuestUserId>]
[EfCoreConverter<OrderId>]
[EfCoreConverter<OrderItemId>]
[EfCoreConverter<Quantity>]
[EfCoreConverter<Price>]
internal partial class VogenEfCoreConverters;
