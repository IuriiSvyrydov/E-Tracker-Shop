using MediatR;

namespace E_Tracker.Application.Features.Commands.Product.UpdateProduct;

public class UpdateProductQueryRequest: IRequest<UpdateProductQueryResponse>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Stock { get; set; }
    public long Price { get; set; }
}