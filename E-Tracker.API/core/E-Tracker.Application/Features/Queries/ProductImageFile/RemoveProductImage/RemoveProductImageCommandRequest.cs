using MediatR;

namespace E_Tracker.Application.Features.Queries.ProductImageFile.RemoveProductImage;

public class RemoveProductImageCommandRequest: IRequest<RemoveProductImageCommandResponse>
{
    public string Id { get; set; }
    public string ImageId { get; set; }
}