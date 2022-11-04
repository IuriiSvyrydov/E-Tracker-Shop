using E_Tracker.Application.Abstractions;
using E_Tracker.Application.Repositories.Customer;
using E_Tracker.Application.Repositories.Order;
using E_Tracker.Application.Repositories.Product;
using E_Tracker.Application.RequestParameters;
using E_Tracker.Application.ViewModels.Producrs;
using E_Tracker.Application.ViewModels.Products;
using E_Tracker.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Tracker.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _readRepository;
        private readonly IProductWriteRepository _writeRepository;
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IWriteCustomerRepository _customerRepository;

        public ProductsController(IProductReadRepository readRepository, IProductWriteRepository writeRepository,
            IOrderWriteRepository orderWriteRepository, IWriteCustomerRepository customerRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _orderWriteRepository = orderWriteRepository;
            _customerRepository = customerRepository;
        }
        
        [HttpGet]
        public async Task <IActionResult> GetProducts([FromQuery]Pagination pagination)
        {
            var totalCount = _readRepository.GetAll(false).Count();
            var products =  _readRepository.GetAll().Skip(pagination.Page * pagination.Size).Take(pagination.Size)
                .Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreateDate,
                p.UpdateDate
            }).ToList();
            return Ok(new
            {
                totalCount,
                products
            });
                
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            var product = await _readRepository.GetByIdAsync(id);
            return Ok(product);

        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductViewModel model)
        {
            await _writeRepository.AddAsync(new()
            {
                Name = model.Name,
                Stock = model.Stock,
                Price = model.Price,
               
            });
            await _writeRepository.SaveChanges();
            return StatusCode((int)StatusCodes.Status201Created);
        }
        [HttpPut]
        public async Task<IActionResult> Put(UpdateProductViewModel model)
        {
            Product product = await _readRepository.GetByIdAsync(model.Id);
            product.Name = model.Name;
            product.Stock = model.Stock;
            product.Price = model.Price;
           
            await _writeRepository.SaveChanges();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _writeRepository.RemoveAsync(id);
            await _writeRepository.SaveChanges();
            return Ok( );
        }
    }
}
