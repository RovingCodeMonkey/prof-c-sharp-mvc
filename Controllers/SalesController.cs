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
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool ascending = true)
        {
            count = Math.Min(count, pagingSettings.Value.MaxPageSize);
            var (items, totalCount) = await sales.Get(page, count, productId, salesPersonId, customerId, startDate, endDate, sortBy, ascending);
            return PagedResult<Sale>.Create(items, totalCount, page, count);
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
            var result = await sales.Create(sale);
            if (!result.IsSuccess)
                return Conflict(result.Error);
            return CreatedAtAction(nameof(Get), new { id = result.Value!.SalesId }, result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Sale sale)
        {
            if (!await sales.Update(sale))
                return NotFound();
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
