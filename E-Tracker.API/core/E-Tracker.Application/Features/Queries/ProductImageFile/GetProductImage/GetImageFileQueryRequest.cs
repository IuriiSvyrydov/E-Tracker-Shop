using MediatR;

namespace E_Tracker.Application.Features.Queries.ProductImageFile.GetProductImage
{
    public class GetImageFileQueryRequest: IRequest<List<GetImageFileQueryResponse>>
    {
        public string Id { get; set; }
    }

    
}
