using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using BaseDotnet.Application.DTOs;
using BaseDotnet.Application.Interfaces;
using BaseDotnet.Domain.Common;
using BaseDotnet.Domain.Entities;
using BaseDotnet.Domain.Interfaces;

namespace BaseDotnet.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateProductDto> _createValidator;
        private readonly IValidator<UpdateProductDto> _updateValidator;

        public ProductService(
            IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<CreateProductDto> createValidator,
            IValidator<UpdateProductDto> updateValidator)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<Result<ProductDto>> CreateAsync(CreateProductDto dto, CancellationToken cancellationToken = default)
        {
            var validationResult = await _createValidator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToList());
                return Result.Failure<ProductDto>(errors, "Product validation failed.");
            }

            var isUnique = await _productRepository.IsSkuUniqueAsync(dto.Sku, cancellationToken);
            if (!isUnique)
            {
                return Result.Failure<ProductDto>(new Dictionary<string, List<string>>
                {
                    { nameof(dto.Sku), new List<string> { "SKU must be unique." } }
                }, "Product creation failed.");
            }

            var product = new Product(Guid.NewGuid(), dto.Name, dto.Description, dto.Price, dto.Sku);
            await _productRepository.AddAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var resultDto = _mapper.Map<ProductDto>(product);
            return Result.Success(resultDto, "Product created successfully.");
        }

        public async Task<Result<ProductDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetByIdAsync(id, cancellationToken);
            if (product == null)
            {
                return Result.Failure<ProductDto>($"Product with ID {id} was not found.");
            }

            var dto = _mapper.Map<ProductDto>(product);
            return Result.Success(dto);
        }

        public async Task<Result<IReadOnlyList<ProductDto>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var products = await _productRepository.GetAllAsync(cancellationToken);
            var dtos = _mapper.Map<IReadOnlyList<ProductDto>>(products);
            return Result.Success(dtos);
        }

        public async Task<Result> UpdateAsync(Guid id, UpdateProductDto dto, CancellationToken cancellationToken = default)
        {
            var validationResult = await _updateValidator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToList());
                return Result.Failure(errors, "Product validation failed.");
            }

            var product = await _productRepository.GetByIdAsync(id, cancellationToken);
            if (product == null)
            {
                return Result.Failure($"Product with ID {id} was not found.");
            }

            if (product.Sku != dto.Sku)
            {
                var isUnique = await _productRepository.IsSkuUniqueAsync(dto.Sku, cancellationToken);
                if (!isUnique)
                {
                    return Result.Failure(new Dictionary<string, List<string>>
                    {
                        { nameof(dto.Sku), new List<string> { "SKU must be unique." } }
                    }, "Product update failed.");
                }
            }

            product.Update(dto.Name, dto.Description, dto.Price, dto.Sku);
            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success("Product updated successfully.");
        }

        public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetByIdAsync(id, cancellationToken);
            if (product == null)
            {
                return Result.Failure($"Product with ID {id} was not found.");
            }

            _productRepository.Delete(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success("Product deleted successfully.");
        }
    }
}
