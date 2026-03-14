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
        public async Task<IEnumerable<SalesPerson>> Get()
        {
            return await salesPersons.Get();
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
