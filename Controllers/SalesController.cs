using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesController(ISales sales) : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<Sale>> Get()
        {
            return await sales.Get();
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
