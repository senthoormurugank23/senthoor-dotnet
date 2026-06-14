using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BaseDotnet.Domain.Entities;
using BaseDotnet.Domain.Interfaces;

namespace BaseDotnet.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : Repository<Product, Guid>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) 
            : base(dbContext)
        {
        }

        public async Task<Product?> GetBySkuAsync(string sku, CancellationToken cancellationToken = default)
        {
            return await DbContext.Products
                .FirstOrDefaultAsync(x => x.Sku == sku, cancellationToken);
        }

        public async Task<bool> IsSkuUniqueAsync(string sku, CancellationToken cancellationToken = default)
        {
            return !await DbContext.Products
                .AnyAsync(x => x.Sku == sku, cancellationToken);
        }
    }
}
