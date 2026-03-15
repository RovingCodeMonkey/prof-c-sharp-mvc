using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController(ICustomers customers, IOptions<PagingSettings> pagingSettings) : ControllerBase
    {
        [HttpGet]
        public async Task<PagedResult<Customer>> Get(
            [FromQuery] int page = 0,
            [FromQuery] int count = 20,
            [FromQuery] string? search = null,
            [FromQuery] string? phone = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool ascending = true)
        {
            count = Math.Min(count, pagingSettings.Value.MaxPageSize);
            var (items, totalCount) = await customers.Get(page, count, search, phone, sortBy, ascending);
            return new PagedResult<Customer>
            {
                Items = items,
                Count = totalCount,
                Cursor = (page + 1) * count < totalCount ? page + 1 : null
            };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            var customer = await customers.Get(id);
            return customer is null ? NotFound() : Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Create(Customer customer)
        {
            var created = await customers.Create(customer);
            return CreatedAtAction(nameof(Get), new { id = created.CustomerId }, created);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Customer customer)
        {
            await customers.Update(customer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await customers.Delete(id);
            return NoContent();
        }
    }
}
