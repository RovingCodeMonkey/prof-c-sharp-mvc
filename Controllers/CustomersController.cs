using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController(ICustomers customers) : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<Customer>> Get()
        {
            return await customers.Get();
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
