using ECommerceAPI.Models;
using ECommerceAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatelogController : ControllerBase
    {

        private readonly IProductRepository productRepository;
        private readonly ILogger<CatelogController> logger;


        public CatelogController(IProductRepository productRepository, ILogger<CatelogController> logger)
        {
            this.productRepository = productRepository;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products  = await productRepository.GetProducts();
            if(products == null)
            {
                return Ok(products);
            }
            else
            {
                return Ok(products);
            }
        }

        [HttpGet("{id:length(24)}",Name ="GetProduct")]
        [ProducesResponseType(typeof(Product),200)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product =  await productRepository.GetProduct(id);

            if(product == null)
            {
                logger.LogError($"Product with id {id}, not found.");   
                return NotFound();
            }
            else
            {
                return Ok(product);
            }
        }


        [HttpGet]
        [Route("[action]/{category}",Name ="GetProductByCategory")]
        [ProducesResponseType(typeof(IEnumerable<Product>),200)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            var products  = await productRepository.GetProductByCategory(category);
            if(products == null)
            {
                logger.LogError($"Product with Category {category}, not found.");
                return NotFound();
            }
            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product),200)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await productRepository.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut(Name ="UpdateProduct")]
        [ProducesResponseType(typeof(Product), 200)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
           return Ok( await productRepository.UpdateProduct(product));
        }

        [HttpDelete("{id:length(24)}",Name ="DeleteProduct")]
        [ProducesResponseType(typeof(Product),200)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            return Ok(await productRepository.DeleteProduct(id));
        }

    }
}
