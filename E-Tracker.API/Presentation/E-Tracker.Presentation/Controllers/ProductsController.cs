

namespace E_Tracker.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
 
        private readonly IMediator _mediator;

        public ProductsController( IMediator mediator)
        {
   
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task <IActionResult> GetProducts([FromQuery]GetAllProductQueryRequest getAllProductQueryRequest)
        {

           var result =  await _mediator.Send(getAllProductQueryRequest);
           return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute]GetProductByIdQueryRequest getProductByIdQueryRequest)
        {
            GetProductByIdQueryResponse response = await _mediator.Send(getProductByIdQueryRequest);
          //  var response =await _readRepository.GetByIdAsync(id,false);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            var response = await _mediator.Send(createProductCommandRequest);

            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateProductQueryRequest request)
        {
            UpdateProductQueryResponse response = await _mediator.Send(request);
           
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute]RemoveProductRequest removeProductRequest)
        {
            RemoveProductResponse response = await _mediator.Send(removeProductRequest);
            return Ok( response);
        }
       
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery]UploadImageFileRequest uploadImageFileRequest)
        {
            uploadImageFileRequest.Files = Request.Form.Files;
            UploadImageFileResponse response = await _mediator.Send(uploadImageFileRequest);
        
            return Ok(response);
        }

        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetProductImages([FromRoute]GetImageFileQueryRequest getImageFileQueryRequest)
        {
            List<GetImageFileQueryResponse> response = await _mediator.Send(getImageFileQueryRequest);
            return Ok(response);

        }

        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> DeleteImage([FromRoute]RemoveProductImageCommandRequest removeProductImageCommandRequest,
            [FromQuery]string imageId)
        {
             removeProductImageCommandRequest.ImageId = imageId;
            var response = await _mediator.Send(removeProductImageCommandRequest);
            return Ok(response);
        }

    }

}
