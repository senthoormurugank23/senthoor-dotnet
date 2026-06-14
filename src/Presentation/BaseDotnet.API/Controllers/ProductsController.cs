using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BaseDotnet.Application.DTOs;
using BaseDotnet.Application.Interfaces;
using BaseDotnet.Domain.Common;

namespace BaseDotnet.API.Controllers
{
    public class ProductsController : ApiControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<ActionResult<Result<ProductDto>>> Create(CreateProductDto dto, CancellationToken cancellationToken)
        {
            var result = await _productService.CreateAsync(dto, cancellationToken);
            return HandleResult(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Result<ProductDto>>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _productService.GetByIdAsync(id, cancellationToken);
            return HandleResult(result);
        }

        [HttpGet]
        public async Task<ActionResult<Result<IReadOnlyList<ProductDto>>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _productService.GetAllAsync(cancellationToken);
            return HandleResult(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Result>> Update(Guid id, UpdateProductDto dto, CancellationToken cancellationToken)
        {
            var result = await _productService.UpdateAsync(id, dto, cancellationToken);
            return HandleResult(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Result>> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _productService.DeleteAsync(id, cancellationToken);
            return HandleResult(result);
        }
    }
}
