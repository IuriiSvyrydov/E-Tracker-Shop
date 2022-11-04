using E_Tracker.Application.Repositories.Product;
using MediatR;

namespace E_Tracker.Application.Features.Commands.Product.CreateProduct;

public class CreateProductCommandHandler: IRequestHandler<CreateProductCommandRequest,CreateProductCommandResponse>
{
    private readonly IProductWriteRepository _writeRepository;

    public CreateProductCommandHandler(IProductWriteRepository writeRepository)
    {
        _writeRepository = writeRepository;
    }

    public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        await _writeRepository.AddAsync(new()
        {
            Name = request.Name,
            Stock = request.Stock,
            Price = request.Price,

        });
       await _writeRepository.SaveChanges();
       return new();
    }
}