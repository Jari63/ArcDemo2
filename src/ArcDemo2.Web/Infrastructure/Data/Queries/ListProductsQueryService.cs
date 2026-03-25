using Microsoft.EntityFrameworkCore;
using ArcDemo2.Web.ProductFeatures;
using ArcDemo2.Web.ProductFeatures.List;

namespace ArcDemo2.Web.Infrastructure.Data.Queries;

public class ListProductsQueryService : IListProductsQueryService
{
  private readonly AppDbContext _db;

  public ListProductsQueryService(AppDbContext db)
  {
    _db = db;
  }

  public async Task<PagedResult<ProductDto>> ListAsync(int page, int perPage)
  {
    var items = await _db.Products
      .OrderBy(p => p.Id)
      .Skip((page - 1) * perPage)
      .Take(perPage)
      .Select(p => new ProductDto(p.Id, p.Name, p.UnitPrice))
      .AsNoTracking()
      .ToListAsync();

    int totalCount = await _db.Products.CountAsync();
    int totalPages = (int)Math.Ceiling(totalCount / (double)perPage);
    var result = new PagedResult<ProductDto>(items, page, perPage, totalCount, totalPages);

    return result;
  }
}
