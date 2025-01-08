using ECommerce.Catalog.Application.Contracts;
using MediatR;

namespace ECommerce.Catalog.Application.Features.Products.Commands.DiscountPrice
{
    public class DiscountProductPriceCommandHandler(IProductRepository productRepository) : IRequestHandler<DiscountProductPriceRequest, DiscountProductPriceResponse>
    {
        public async Task<DiscountProductPriceResponse> Handle(DiscountProductPriceRequest request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.ProductId);
            if (product is null)
            {
                return new DiscountProductPriceResponse(false, $"{request.ProductId} id'li ürün bulunamadı");
            }

            product.AppyDiscount(request.DiscountRate);
            await productRepository.UpdateAsync(product);

            return new DiscountProductPriceResponse(true,  "Ürün fiyatı başarıyla güncellendi");

        }
    }
}
