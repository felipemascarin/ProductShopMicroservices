using GeekShooping.ProductAPI.Data.ValueObjects;
using GeekShooping.ProductAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekShooping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _repository;
        public ProductController(IProductRepository repository)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct([FromBody] ProductDto productDto)
        {
            if (productDto == null) return BadRequest();
            return Ok(await _repository.Post(productDto));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await _repository.GetAll();
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductDto>> GetById(long productId)
        {
            var product = await _repository.GetById(productId);
            if (product.Id <= 0) return NotFound();
            return Ok(product);
        }

        [HttpPut]
        public async Task<ActionResult<ProductDto>> PutProduct([FromBody] ProductDto productDto)
        {
            if (productDto == null) return BadRequest();
            return Ok(await _repository.Put(productDto));
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(long productId)
        {
            if (await _repository.Delete(productId) == false) return NotFound();
            else return Ok();
        }
    }
}