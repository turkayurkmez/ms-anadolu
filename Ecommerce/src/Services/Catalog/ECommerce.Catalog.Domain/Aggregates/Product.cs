using ECommerce.Catalog.Domain.Events;
using ECommerce.Shared.Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Domain.Aggregates
{
    public  class Product : AggregateRoot<Guid>
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public decimal Price { get; private set; }

        public int Stock { get; private set; }

        public int? CategoryId { get; private set; }

        public string ImageUrl { get; set; } = "noimage.png";

        public Category? Category { get; set; }

        public Product()
        {
            
        }

        public Product(string name, string description, decimal price, int stock, int? categoryId, string imageUrl)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            CategoryId = categoryId;
            ImageUrl = imageUrl;
        }

        public void Update(string name, string description, decimal price, int stock, int? categoryId, string imageUrl)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            CategoryId = categoryId;
            ImageUrl = imageUrl;
        }

        public void AppyDiscount(decimal discountRate)
        {
            //Burada fiyatı güncelleyeceğiz ve bir domain event ekleyeceğiz.
            var oldPrice = Price;
            Price  = Price * (1 - discountRate);
            //Dinleyen herkese söyle: "Benim fiyatım indirildi."
            AddDomainEvent(new ProductDiscountedDomainEvent(Id, oldPrice, Price));

        }
    }
}
