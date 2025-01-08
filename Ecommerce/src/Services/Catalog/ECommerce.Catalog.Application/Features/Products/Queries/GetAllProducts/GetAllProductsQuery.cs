using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Application.Features.Products.Queries.GetAllProducts
{
    public record GetAllProductsQuery : IRequest<IEnumerable<ProductDisplayResponse>>;


    public record ProductDisplayResponse(Guid Id, string Name, string Description, decimal Price, int? CategoryId, string ImageUrl = "noImage.png" );

}
