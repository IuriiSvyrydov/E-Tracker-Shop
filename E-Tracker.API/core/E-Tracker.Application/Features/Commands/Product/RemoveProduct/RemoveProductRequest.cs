using MediatR;

namespace E_Tracker.Application.Features.Commands.Product.RemoveProduct;

public class RemoveProductRequest: IRequest<RemoveProductResponse>
{
    public string Id { get; set; }
}