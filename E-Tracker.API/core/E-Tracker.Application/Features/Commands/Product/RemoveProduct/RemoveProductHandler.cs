using E_Tracker.Application.Repositories;
using E_Tracker.Application.Repositories.Product;
using MediatR;

namespace E_Tracker.Application.Features.Commands.Product.RemoveProduct;

public class RemoveProductHandler: IRequestHandler<RemoveProductRequest,RemoveProductResponse>
{
    private readonly IProductWriteRepository _writeRepository;

    public RemoveProductHandler(IProductWriteRepository writeRepository)
    {
        _writeRepository = writeRepository;
    }

    public async Task<RemoveProductResponse> Handle(RemoveProductRequest request, CancellationToken cancellationToken)
    {
        await _writeRepository.RemoveAsync(request.Id);
        await _writeRepository.SaveChanges();
        return new();
    }
}