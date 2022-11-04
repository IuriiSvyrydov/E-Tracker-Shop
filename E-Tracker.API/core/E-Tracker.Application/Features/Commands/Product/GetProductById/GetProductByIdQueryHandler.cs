using E_Tracker.Application.Repositories.Product;
using MediatR;
namespace E_Tracker.Application.Features.Commands.Product.GetProductById;

public class GetProductByIdQueryHandler: IRequestHandler<GetProductByIdQueryRequest,GetProductByIdQueryResponse>
{
    private readonly IProductReadRepository _readRepository;

    public GetProductByIdQueryHandler(IProductReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQueryRequest request,
        CancellationToken cancellationToken)
    {
           Domain.Entities.Product product = await _readRepository.GetByIdAsync(request.Id,false);
          return new()
          {
              Name = product.Name,
              Stock = product.Stock,
              Price = product.Price,
          };
        
         


          



    }
}