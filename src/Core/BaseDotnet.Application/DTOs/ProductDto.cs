using System;

namespace BaseDotnet.Application.DTOs
{
    public record ProductDto(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        string Sku,
        DateTime CreatedAtUtc,
        string CreatedBy,
        DateTime? LastModifiedAtUtc,
        string? LastModifiedBy
    );
}
