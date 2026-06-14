namespace BaseDotnet.Application.DTOs
{
    public record CreateProductDto(
        string Name,
        string Description,
        decimal Price,
        string Sku
    );
}
