using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiscountsController(IDiscounts discounts, IOptions<PagingSettings> pagingSettings) : ControllerBase
    {
        [HttpGet]
        public async Task<PagedResult<Discount>> Get(
            [FromQuery] int page = 0,
            [FromQuery] int count = 20,
            [FromQuery] string? search = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool ascending = true)
        {
            count = Math.Min(count, pagingSettings.Value.MaxPageSize);
            var (items, totalCount) = await discounts.Get(page, count, search, sortBy, ascending);
            return new PagedResult<Discount>
            {
                Items = items,
                Count = totalCount,
                Cursor = (page + 1) * count < totalCount ? page + 1 : null
            };
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<Discount>>> GetAvailable(
            [FromQuery] long productId,
            [FromQuery] DateTime date)
        {
            return Ok(await discounts.GetAvailable(productId, date));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Discount>> Get(int id)
        {
            var discount = await discounts.Get(id);
            return discount is null ? NotFound() : Ok(discount);
        }

        [HttpPost]
        public async Task<ActionResult<Discount>> Create(Discount discount)
        {
            var created = await discounts.Create(discount);
            return CreatedAtAction(nameof(Get), new { id = created.DiscountId }, created);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Discount discount)
        {
            await discounts.Update(discount);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await discounts.Delete(id);
            return NoContent();
        }
    }
}
