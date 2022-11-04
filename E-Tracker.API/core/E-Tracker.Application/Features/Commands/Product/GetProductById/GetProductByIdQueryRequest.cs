using MediatR;

namespace E_Tracker.Application.Features.Commands.Product.GetProductById;

public class GetProductByIdQueryRequest: IRequest<GetProductByIdQueryResponse>
{
    public string Id  { get; set; }

}