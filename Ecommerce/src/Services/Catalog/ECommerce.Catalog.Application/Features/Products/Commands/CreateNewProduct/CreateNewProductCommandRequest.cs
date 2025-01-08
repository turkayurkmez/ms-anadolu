using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Application.Features.Products.Commands.CreateNewProduct
{
    public record CreateNewProductCommandRequest(string Name, string Description, decimal Price, int Stock, int? CategoryId, string ImageUrl="noImage.png"): IRequest<Guid>;



}
