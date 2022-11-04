using E_Tracker.Application.Repositories.Product;
using E_Tracker.Application.Repositories.ProductImageFile;
using E_Tracker.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace E_Tracker.Application.Features.Queries.ProductImageFile.GetProductImage;

public class GetImageFileQueryHandler: IRequestHandler<GetImageFileQueryRequest, List<GetImageFileQueryResponse>>
{
    private readonly IProductReadRepository _readRepository;
    private readonly IConfiguration _configuration;


    public GetImageFileQueryHandler(IProductReadRepository readRepository, IConfiguration configuration)
    {
        _readRepository = readRepository;
        _configuration = configuration;
    }

    public async Task<List<GetImageFileQueryResponse>> Handle(GetImageFileQueryRequest request, CancellationToken cancellationToken)
    {
        Product? product = await _readRepository.Table.Include(p => p.ProductImageFiles)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse((request.Id)));
        return (product.ProductImageFiles.Select(p => new GetImageFileQueryResponse
        {
            Path = $"{_configuration["BaseStorageUrl"]}/{p.Path}",
            FileName = p.FileName,
            Id = p.Id
        }).ToList());
    }
}