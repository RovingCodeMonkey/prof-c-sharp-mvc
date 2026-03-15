using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesPersonsController(ISalesPersons salesPersons) : ControllerBase
    {
        [HttpGet]
        public async Task<PagedResult<SalesPerson>> Get(
            [FromQuery] int page = 0,
            [FromQuery] int count = 20,
            [FromQuery] string? search = null,
            [FromQuery] string? phone = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool ascending = true)
        {
            count = Math.Min(count, 50);
            var (items, totalCount) = await salesPersons.Get(page, count, search, phone, sortBy, ascending);
            return new PagedResult<SalesPerson>
            {
                Items = items,
                Count = totalCount,
                Cursor = (page + 1) * count < totalCount ? page + 1 : null
            };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalesPerson>> Get(int id)
        {
            var salesPerson = await salesPersons.Get(id);
            return salesPerson is null ? NotFound() : Ok(salesPerson);
        }

        [HttpPost]
        public async Task<ActionResult<SalesPerson>> Create(SalesPerson salesPerson)
        {
            var created = await salesPersons.Create(salesPerson);
            return CreatedAtAction(nameof(Get), new { id = created.SalesPersonId }, created);
        }

        [HttpPut]
        public async Task<IActionResult> Update(SalesPerson salesPerson)
        {
            await salesPersons.Update(salesPerson);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await salesPersons.Delete(id);
            return NoContent();
        }
    }
}
