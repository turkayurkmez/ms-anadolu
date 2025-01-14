using ECommerce.Catalog.Application.Contracts;
using ECommerce.Catalog.Domain.Aggregates;
using ECommerce.Catalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
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

        public async Task AddAsync(Product entity)
        {
            await catalogDbContext.Products.AddAsync(entity);
            await catalogDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product entity)
        {
            catalogDbContext.Products.Remove(entity);
            await catalogDbContext.SaveChangesAsync();

        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return await catalogDbContext.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await catalogDbContext.Products.FindAsync(id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await catalogDbContext.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
        }

        public async Task<IReadOnlyList<Product>> SearchByNameAsync(string name)
        {
            return await catalogDbContext.Products.Where(p => p.Name.Contains(name)).ToListAsync();
        }

        public async Task UpdateAsync(Product entity)
        {


            //await using var transaction = await catalogDbContext.Database.BeginTransactionAsync();
            try
            {
                catalogDbContext.Products.Update(entity);
                //    await catalogDbContext.SaveChangesAsync();
                //  await transaction.CommitAsync();
            }
            catch (Exception)
            {
                //await transaction.RollbackAsync();
                throw;
            }

            await catalogDbContext.SaveChangesAsync();

        }
    }
}
