using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportsController(ISales sales) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ReportResult>> Get(
            [FromQuery] int? year = null,
            [FromQuery] int? quarter = null,
            [FromQuery] long? productId = null,
            [FromQuery] long? salesPersonId = null,
            [FromQuery] long? customerId = null)
        {
            var now = DateTime.UtcNow;
            var y = year    ?? now.Year;
            var q = quarter ?? (now.Month - 1) / 3 + 1;

            if (q < 1 || q > 4)
                return BadRequest("Quarter must be between 1 and 4.");
            if (y < 1 || y > 9999)
                return BadRequest("Year is out of range.");

            var startMonth = (q - 1) * 3 + 1;
            var endMonth   = q * 3;
            var startDate  = new DateTime(y, startMonth, 1);
            var endDate    = new DateTime(y, endMonth, DateTime.DaysInMonth(y, endMonth));

            return Ok(await sales.GetReport(productId, salesPersonId, customerId, startDate, endDate));
        }
    }
}
