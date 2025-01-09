using ECommerce.Catalog.Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Application.Features.Products.Queries.GetProduct
{
    public record GetProductByIdQuery(Guid Id): IRequest<ProductResponse>;

    public record ProductResponse(Guid Id, string Name, string Description, decimal Price, int Stock, int? CategoryId, string ImageUrl);

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            return new ProductResponse(product.Id, product.Name, product.Description, product.Price, product.Stock, product.CategoryId, product.ImageUrl);
        }
    }

}
