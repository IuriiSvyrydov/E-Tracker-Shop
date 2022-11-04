using E_Tracker.Application.Abstractions;
using E_Tracker.Application.Repositories.ProductImageFile;
using E_Tracker.Application.Repositories.Product;
using MediatR;

namespace E_Tracker.Application.Features.Commands.Product.ProductImageFile.UploadImageFile;

public class UploadImageFileHandler: IRequestHandler<UploadImageFileRequest,UploadImageFileResponse>
{
    private readonly IStorageService _storageService;
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
    private readonly IProductReadRepository _readRepository;

    public UploadImageFileHandler(IStorageService storageService, IProductImageFileWriteRepository productImageFileWriteRepository, IProductReadRepository readRepository)
    {
        _storageService = storageService;
        _productImageFileWriteRepository = productImageFileWriteRepository;
        _readRepository = readRepository;
    }

    public async Task<UploadImageFileResponse> Handle(UploadImageFileRequest request,
        CancellationToken cancellationToken)
    {
        List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("photo-images",request.Files );
        Domain.Entities.Product product = await _readRepository.GetByIdAsync((request.Id), false);
        await _productImageFileWriteRepository.AddRangeAsync(result.Select(f => new Domain.Entities.ProductImageFile
        {
            FileName = f.fileName,
            Path = f.pathOrContainerName,
            Srorage = _storageService.StorageName,
            Products = new List<Domain.Entities.Product>() { product }

        }).ToList());
        await _productImageFileWriteRepository.SaveChanges();
        return new();
    }
}