using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController(IProducts products, IOptions<PagingSettings> pagingSettings) : ControllerBase
    {

        [HttpGet]
        public async Task<PagedResult<Product>> Get(
            [FromQuery] int page = 0,
            [FromQuery] int count = 20,
            [FromQuery] string? search = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool ascending = true,
            [FromQuery] bool inStockOnly = false)
        {
            count = Math.Min(count, pagingSettings.Value.MaxPageSize);
            var (items, totalCount) = await products.Get(page, count, search, sortBy, ascending, inStockOnly);
            return PagedResult<Product>.Create(items, totalCount, page, count);
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
            if (await products.ExistsWithNameAndManufacturer(product.Name, product.Manufacturer))
                return Conflict("A product with the same name and manufacturer already exists.");
            var created = await products.Create(product);
            return CreatedAtAction(nameof(Get), new { id = created.ProductId }, created);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            if (await products.ExistsWithNameAndManufacturer(product.Name, product.Manufacturer, product.ProductId))
                return Conflict("A product with the same name and manufacturer already exists.");
            if (!await products.Update(product))
                return NotFound();
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
