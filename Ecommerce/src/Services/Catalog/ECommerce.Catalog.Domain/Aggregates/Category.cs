using ECommerce.Shared.Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Domain.Aggregates
{
    public class Category : AggregateRoot<int>
    {
        public string Name { get; set; }

        public Category()
        {
                
        }

        public Category(string name)
        {
            Name = name;
        }

        public void Update(string name)
        {
            Name = name;
        }

        //Varolan kategoriye yeni ürün eklenebilir ve olay fırlatılabilir.
        public IList<Product> Products { get; set; }
    }
}
