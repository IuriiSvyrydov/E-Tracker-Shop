using E_Tracker.Application.Abstractions.Hubs;
using E_Tracker.Application.Repositories.Product;
using MediatR;

namespace E_Tracker.Application.Features.Commands.Product.CreateProduct;

public class CreateProductCommandHandler: IRequestHandler<CreateProductCommandRequest,CreateProductCommandResponse>
{
    private readonly IProductWriteRepository _writeRepository;
    private readonly IHubProductService _hubProductService;

    public CreateProductCommandHandler(IProductWriteRepository writeRepository, IHubProductService hubProductService)
    {
        _writeRepository = writeRepository;
        _hubProductService = hubProductService;
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
       await _hubProductService.AddProductMessageAsync($"{request.Name} Product was created");
       return new();
    }
}