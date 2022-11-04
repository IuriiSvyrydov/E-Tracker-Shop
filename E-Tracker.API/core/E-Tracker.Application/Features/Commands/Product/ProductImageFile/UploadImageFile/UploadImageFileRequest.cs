using MediatR;
using Microsoft.AspNetCore.Http;

namespace E_Tracker.Application.Features.Commands.Product.ProductImageFile.UploadImageFile;

public class UploadImageFileRequest: IRequest<UploadImageFileResponse>
{
    public string Id { get; set; }
    public IFormFileCollection? Files { get; set; }
}