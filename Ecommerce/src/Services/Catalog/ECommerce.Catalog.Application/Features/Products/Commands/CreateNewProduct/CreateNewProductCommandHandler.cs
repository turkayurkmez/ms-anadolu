using ECommerce.Catalog.Application.Contracts;
using ECommerce.Catalog.Domain.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Application.Features.Products.Commands.CreateNewProduct
{
    public class CreateNewProductCommandHandler : IRequestHandler<CreateNewProductCommandRequest, Guid>
    {

        private readonly IProductRepository _productRepository;

        public CreateNewProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Guid> Handle(CreateNewProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Name, request.Description, request.Price, request.Stock, request.CategoryId, request.ImageUrl);

            await _productRepository.AddAsync(product);

            return product.Id;

            //burada db işlemleri yapılacak..

        }
    }
}
