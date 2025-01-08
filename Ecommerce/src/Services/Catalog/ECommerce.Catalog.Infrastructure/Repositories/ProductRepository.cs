using ECommerce.Catalog.Application.Contracts;
using ECommerce.Catalog.Domain.Aggregates;
using ECommerce.Catalog.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly CatalogDbContext catalogDbContext;

        public ProductRepository(CatalogDbContext catalogDbContext)
        {
            this.catalogDbContext = catalogDbContext;
        }

        public Task AddAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Product>> SearchByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
