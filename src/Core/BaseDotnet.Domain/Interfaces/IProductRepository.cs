using System;
using System.Threading;
using System.Threading.Tasks;
using BaseDotnet.Domain.Entities;

namespace BaseDotnet.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<Product?> GetBySkuAsync(string sku, CancellationToken cancellationToken = default);
        Task<bool> IsSkuUniqueAsync(string sku, CancellationToken cancellationToken = default);
    }
}
