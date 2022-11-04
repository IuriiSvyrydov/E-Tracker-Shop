using E_Tracker.Application.Repositories.Product;
using MediatR;

namespace E_Tracker.Application.Features.Queries.GetAllProduct;

public class GetAllProductQueryHandler: IRequestHandler<GetAllProductQueryRequest,GetAllProductResponse>
{
    private readonly IProductReadRepository _readRepository;

    public GetAllProductQueryHandler(IProductReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<GetAllProductResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
    {
        var totalCount = _readRepository.GetAll(false).Count();
        var products = _readRepository.GetAll().Skip(request.Page * request.Size).Take(request.Size)
        .Select(p => new
        {
            p.Id,
            p.Name,
            p.Stock,
            p.Price,
            p.CreateDate,
            p.UpdateDate
            }).ToList();
        return new()
        {
            Products = products,
            TotalCount = totalCount
        };
    }
}