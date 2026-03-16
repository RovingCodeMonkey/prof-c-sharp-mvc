using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesPersonsController(ISalesPersons salesPersons, IOptions<PagingSettings> pagingSettings) : ControllerBase
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
            count = Math.Min(count, pagingSettings.Value.MaxPageSize);
            var (items, totalCount) = await salesPersons.Get(page, count, search, phone, sortBy, ascending);
            return PagedResult<SalesPerson>.Create(items, totalCount, page, count);
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
            if (await salesPersons.ExistsWithNameAndPhone(salesPerson.FirstName, salesPerson.LastName, salesPerson.Phone))
                return Conflict("A salesperson with the same first name, last name, and phone already exists.");
            var created = await salesPersons.Create(salesPerson);
            return CreatedAtAction(nameof(Get), new { id = created.SalesPersonId }, created);
        }

        [HttpPut]
        public async Task<IActionResult> Update(SalesPerson salesPerson)
        {
            if (await salesPersons.ExistsWithNameAndPhone(salesPerson.FirstName, salesPerson.LastName, salesPerson.Phone, salesPerson.SalesPersonId))
                return Conflict("A salesperson with the same first name, last name, and phone already exists.");
            if (!await salesPersons.Update(salesPerson))
                return NotFound();
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
