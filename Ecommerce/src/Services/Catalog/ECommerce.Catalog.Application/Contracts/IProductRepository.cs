using ECommerce.Catalog.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Application.Contracts
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<IReadOnlyList<Product>> GetProductsByCategoryIdAsync(int categoryId);

        Task<IReadOnlyList<Product>> SearchByNameAsync(string name);


    }
}
