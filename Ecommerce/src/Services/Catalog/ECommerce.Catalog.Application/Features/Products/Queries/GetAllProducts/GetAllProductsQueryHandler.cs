using ECommerce.Catalog.Application.Contracts;
using Mapster;
using MediatR;

namespace ECommerce.Catalog.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDisplayResponse>>
    {
        private readonly IProductRepository _productRepository;
        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<ProductDisplayResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();

            //Mapster kullanılarak IReadOnlyList<Product> listesi IEnumerable<ProductDisplayResponse> listesine dönüştürüldü.

            var response = products.Adapt<IEnumerable<ProductDisplayResponse>>();

            //return products.Select(p => new ProductDisplayResponse(p.Id, p.Name, p.Description, p.Price, p.CategoryId, p.ImageUrl));

            return response;
        }
    }

}
