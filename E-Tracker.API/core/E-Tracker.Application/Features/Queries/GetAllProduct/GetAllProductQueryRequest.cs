using E_Tracker.Application.RequestParameters;
using MediatR;

namespace E_Tracker.Application.Features.Queries.GetAllProduct;

public class GetAllProductQueryRequest: IRequest<GetAllProductResponse>, IRequest<Unit>
{
    //public Pagination  Pagination { get; set; }
    public int Page { get; set; } = 0;
    public int Size { get; set; } = 5;
}