using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController(IProducts products) : ControllerBase
    {       

        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await products.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await products.Get(id);
            return product is null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            var created = await products.Create(product);
            return CreatedAtAction(nameof(Get), new { id = created.ProductId }, created);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            await products.Update(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await products.Delete(id);
            return NoContent();
        }
    }
}
