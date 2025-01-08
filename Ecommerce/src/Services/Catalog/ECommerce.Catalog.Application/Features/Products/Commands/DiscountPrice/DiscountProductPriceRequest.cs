using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Application.Features.Products.Commands.DiscountPrice
{
    public  record DiscountProductPriceRequest(Guid ProductId, decimal DiscountRate):IRequest<DiscountProductPriceResponse>;

    public record DiscountProductPriceResponse(bool IsSuccess, string Message);
}
