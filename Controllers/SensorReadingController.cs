using Microsoft.AspNetCore.Mvc;
using GardenService.Models;
using Microsoft.EntityFrameworkCore;

namespace GardenService.Controllers
{

    [ApiController]
    [Route("[controller]")]   
    public class SensorReadingController : ControllerBase
    {

        private readonly GardenServicesDbContext? _gardenDbContext;
        private readonly ILogger<SensorReadingController>? _logger;

        public SensorReadingController(ILogger<SensorReadingController> logger, GardenServicesDbContext gardenServicesDbContext)
        {
            this._logger = logger;
            this._gardenDbContext = gardenServicesDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensorReading>>> GetSensorReadings()
        {
            return Ok(await _gardenDbContext!.SensorReadings.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SensorReading>> GetSensorReading(int id)
        {
            var reading = await _gardenDbContext!.SensorReadings.FindAsync(id);

            if (reading == null)
            {
                return NotFound();
            }

            return Ok(reading);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutSensorReading(int id, SensorReading reading)
        {
            if (id != reading.IX_SensorReading)
            {
                return BadRequest();
            }

            _gardenDbContext!.Entry(reading).State = EntityState.Modified;

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
        public async Task<ActionResult<SensorReading>> PostSensorReading(SensorReading reading)
        {
            _gardenDbContext!.SensorReadings.Add(reading);
            await _gardenDbContext!.SaveChangesAsync();

            return CreatedAtAction("GetSensorReading", new { id = reading.IX_SensorReading }, reading);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SensorReading>> DeleteSensorReading(int id)
        {
            var reading = await _gardenDbContext!.SensorReadings.FindAsync(id);
            if (reading == null)
            {
                return NotFound();
            }

            _gardenDbContext!.SensorReadings.Remove(reading);
            await _gardenDbContext!.SaveChangesAsync();

            return Ok(reading);
        }

        private bool IsOrderExists(int id)
        {
            return _gardenDbContext!.SensorReadings.Any(e => e.IX_SensorReading == id);
        }

    }
}