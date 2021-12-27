using Microsoft.AspNetCore.Mvc;
using GardenService.Models;
using Microsoft.EntityFrameworkCore;

namespace GardenService.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SensorTypeController : ControllerBase
    {

        private readonly GardenServicesDbContext? _gardenDbContext;
        private readonly ILogger<SensorTypeController>? _logger;

        public SensorTypeController(ILogger<SensorTypeController> logger, GardenServicesDbContext gardenServicesDbContext)
        {
            this._logger = logger;
            this._gardenDbContext = gardenServicesDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensorType>>> GetSensorTypes()
        {
            return Ok(await _gardenDbContext!.SensorTypes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SensorType>> GetSensorType(int id)
        {
            var reading = await _gardenDbContext!.SensorTypes.FindAsync(id);

            if (reading == null)
            {
                return NotFound();
            }

            return Ok(reading);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutSensorType(int id, SensorType model)
        {
            if (id != model.IX_SensorType)
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
                if (!IsSensorTypeExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<SensorType>> PostSensorType(SensorType model)
        {
            _gardenDbContext!.SensorTypes.Add(model);
            await _gardenDbContext!.SaveChangesAsync();

            return CreatedAtAction("GetSensorType", new { id = model.IX_SensorType }, model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SensorType>> DeleteSensorType(int id)
        {
            var model = await _gardenDbContext!.SensorTypes.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _gardenDbContext!.SensorTypes.Remove(model);
            await _gardenDbContext!.SaveChangesAsync();

            return Ok(model);
        }

        private bool IsSensorTypeExists(int id)
        {
            return _gardenDbContext!.SensorTypes.Any(e => e.IX_SensorType == id);
        }

    }
}