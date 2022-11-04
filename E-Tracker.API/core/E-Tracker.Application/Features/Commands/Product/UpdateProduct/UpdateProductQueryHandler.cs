using E_Tracker.Application.Repositories;
using E_Tracker.Application.Repositories.Product;
using MediatR;

namespace E_Tracker.Application.Features.Commands.Product.UpdateProduct;


public class UpdateProductQueryHandler: IRequestHandler<UpdateProductQueryRequest,UpdateProductQueryResponse>
{
    private readonly IProductWriteRepository _writeRepository;
    private readonly IProductReadRepository _readRepository;

    public UpdateProductQueryHandler(IProductWriteRepository writeRepository, IProductReadRepository readRepository)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
    }

    public async Task<UpdateProductQueryResponse> Handle(UpdateProductQueryRequest request, CancellationToken cancellationToken)
    {
        Domain.Entities.Product product = await _readRepository.GetByIdAsync(request.Id);
        product.Name = request.Name;
        product.Stock = request.Stock;
        product.Price = request.Price;

      await _writeRepository.SaveChanges();
      return new();
    }
}