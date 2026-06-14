using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BaseDotnet.Application.DTOs;
using BaseDotnet.Domain.Common;

namespace BaseDotnet.Application.Interfaces
{
    public interface IProductService
    {
        Task<Result<ProductDto>> CreateAsync(CreateProductDto dto, CancellationToken cancellationToken = default);
        Task<Result<ProductDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Result<IReadOnlyList<ProductDto>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Result> UpdateAsync(Guid id, UpdateProductDto dto, CancellationToken cancellationToken = default);
        Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
