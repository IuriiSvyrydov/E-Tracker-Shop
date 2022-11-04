using E_Tracker.Application.Repositories.Product;
using E_Tracker.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E_Tracker.Application.Features.Queries.ProductImageFile.RemoveProductImage;

public class RemoveProductImageCommandHandler: IRequestHandler<RemoveProductImageCommandRequest,RemoveProductImageCommandResponse>
{
    private readonly IProductWriteRepository _writeRepository;
    private readonly IProductReadRepository _readRepository;

    public RemoveProductImageCommandHandler(IProductWriteRepository writeRepository, IProductReadRepository readRepository)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
    }

    public async Task<RemoveProductImageCommandResponse> Handle(RemoveProductImageCommandRequest request, CancellationToken cancellationToken)
    {
        Product? product = await _readRepository.Table.Include(p => p.ProductImageFiles)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse((request.Id)));
        Domain.Entities.ProductImageFile? productImageFile =
            product?.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(request.ImageId));
        if(productImageFile!= null)
         product.ProductImageFiles.Remove(productImageFile);
        await _writeRepository.SaveChanges();
        return new();
    }
}