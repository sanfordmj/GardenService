using Microsoft.AspNetCore.Mvc;
using GardenService.Models;
using Microsoft.EntityFrameworkCore;

namespace GardenService.Controllers
{

    [ApiController]
    [Route("[controller]")]   
    public class TraceController : ControllerBase
    {

        private readonly GardenServicesDbContext? _gardenDbContext;
        private readonly ILogger<TraceController>? _logger;

        public TraceController(ILogger<TraceController> logger, GardenServicesDbContext gardenServicesDbContext)
        {
            this._logger = logger;
            this._gardenDbContext = gardenServicesDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trace>>> GetTraces()
        {
            return Ok(await _gardenDbContext!.Traces.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Trace>> GetTrace(int id)
        {
            var reading = await _gardenDbContext!.Traces.FindAsync(id);

            if (reading == null)
            {
                return NotFound();
            }

            return Ok(reading);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrace(int id, Trace model)
        {
            if (id != model.IX_Trace)
            {
                return BadRequest();
            }

            _gardenDbContext!.Entry(model).State = EntityState.Modified;

            try
            {
                await _gardenDbContext!.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsOrderExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<Trace>> PostSensorReading(Trace model)
        {
            _gardenDbContext!.Traces.Add(model);
            await _gardenDbContext!.SaveChangesAsync();

            return CreatedAtAction("GetTrace", new { id = model.IX_Trace }, model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Trace>> DeleteSensorReading(int id)
        {
            var model = await _gardenDbContext!.Traces.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _gardenDbContext!.Traces.Remove(model);
            await _gardenDbContext!.SaveChangesAsync();

            return Ok(model);
        }

        private bool IsOrderExists(int id)
        {
            return _gardenDbContext!.Traces.Any(e => e.IX_Trace == id);
        }

    }
}