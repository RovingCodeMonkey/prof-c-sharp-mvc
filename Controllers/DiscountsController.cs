using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiscountsController(IDiscounts discounts) : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<Discount>> Get()
        {
            return await discounts.Get();
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
