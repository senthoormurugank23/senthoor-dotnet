using System;
using BaseDotnet.Domain.Common;

namespace BaseDotnet.Domain.Entities
{
    public class Product : BaseEntity<Guid>
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public string Sku { get; private set; } = string.Empty;

        // Required by EF Core
        private Product() { }

        public Product(Guid id, string name, string description, decimal price, string sku)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Sku = sku;
        }

        public void Update(string name, string description, decimal price, string sku)
        {
            Name = name;
            Description = description;
            Price = price;
            Sku = sku;
        }
    }
}
