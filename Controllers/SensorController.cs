using Microsoft.AspNetCore.Mvc;
using GardenService.Models;
using Microsoft.EntityFrameworkCore;

namespace GardenService.Controllers
{

    [ApiController]
    [Route("[controller]")]   
    public class SensorController : ControllerBase
    {

        private readonly GardenServicesDbContext? _gardenDbContext;
        private readonly ILogger<SensorController>? _logger;

        public SensorController(ILogger<SensorController> logger, GardenServicesDbContext gardenServicesDbContext)
        {
            this._logger = logger;
            this._gardenDbContext = gardenServicesDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sensor>>> GetSensors()
        {
            return Ok(await _gardenDbContext!.Sensors.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sensor>> GetSensor(int id)
        {
            var reading = await _gardenDbContext!.Sensors.FindAsync(id);

            if (reading == null)
            {
                return NotFound();
            }

            return Ok(reading);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutSensor(int id, Sensor model)
        {
            if (id != model.IX_Sensor)
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
        public async Task<ActionResult<Sensor>> PostSensorReading(Sensor model)
        {
            _gardenDbContext!.Sensors.Add(model);
            await _gardenDbContext!.SaveChangesAsync();

            return CreatedAtAction("GetSensor", new { id = model.IX_Sensor }, model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Sensor>> DeleteSensorReading(int id)
        {
            var model = await _gardenDbContext!.Sensors.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _gardenDbContext!.Sensors.Remove(model);
            await _gardenDbContext!.SaveChangesAsync();

            return Ok(model);
        }

        private bool IsOrderExists(int id)
        {
            return _gardenDbContext!.Sensors.Any(e => e.IX_Sensor == id);
        }

    }
}