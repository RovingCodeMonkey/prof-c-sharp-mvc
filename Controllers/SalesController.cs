using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesController(ISales sales, IOptions<PagingSettings> pagingSettings) : ControllerBase
    {
        [HttpGet]
        public async Task<PagedResult<Sale>> Get(
            [FromQuery] int page = 0,
            [FromQuery] int count = 20,
            [FromQuery] long? productId = null,
            [FromQuery] long? salesPersonId = null,
            [FromQuery] long? customerId = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool ascending = true)
        {
            count = Math.Min(count, pagingSettings.Value.MaxPageSize);
            var (items, totalCount) = await sales.Get(page, count, productId, salesPersonId, customerId, sortBy, ascending);
            return new PagedResult<Sale>
            {
                Items = items,
                Count = totalCount,
                Cursor = (page + 1) * count < totalCount ? page + 1 : null
            };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> Get(int id)
        {
            var sale = await sales.Get(id);
            return sale is null ? NotFound() : Ok(sale);
        }

        [HttpPost]
        public async Task<ActionResult<Sale>> Create(Sale sale)
        {
            var created = await sales.Create(sale);
            return CreatedAtAction(nameof(Get), new { id = created.SalesId }, created);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Sale sale)
        {
            await sales.Update(sale);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await sales.Delete(id);
            return NoContent();
        }
    }
}
