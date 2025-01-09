using ECommerce.Catalog.Application.Features.Products.Commands.CreateNewProduct;
using ECommerce.Catalog.Application.Features.Products.Commands.DiscountPrice;
using ECommerce.Catalog.Application.Features.Products.Queries.GetAllProducts;
using ECommerce.Catalog.Application.Features.Products.Queries.GetProduct;
using MediatR;

namespace ECommerce.Catalog.API
{
    public static class ProductsEndPoints 
    {
        public static void UseProductsEndPoints(this IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup("products")
                                 .WithTags("Products")
                                 .WithOpenApi();

            group.MapGet("/", async (IMediator mediator) =>
            {
                var request = new GetAllProductsQuery();
                var response = await mediator.Send(request);

                return Results.Ok(response);

            });

            group.MapPut("/{id}/discount/{discount}", async (IMediator mediator, Guid productId, decimal discount) =>
            {
                var request = new DiscountProductPriceRequest(productId, discount);
                var response = await mediator.Send(request);
                return response.IsSuccess ? Results.Ok(response) : Results.NotFound(response.Message);

            });

            group.MapPost("/", async (IMediator mediator, CreateNewProductCommandRequest request) =>
            {
                var response = await mediator.Send(request);
                return Results.Created($"/products/{response}", response);
            });

            group.MapGet("/{id}", async (IMediator mediator, Guid id) =>
            {
                var request = new GetProductByIdQuery(id);
                var response = await mediator.Send(request);
                return response is not null ? Results.Ok(response) : Results.NotFound();
            });

        }
        
    }
}
