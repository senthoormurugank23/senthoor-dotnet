using AutoMapper;
using BaseDotnet.Application.DTOs;
using BaseDotnet.Domain.Entities;

namespace BaseDotnet.Application.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
