using Microsoft.AspNetCore.Mvc;
using GardenService.Models;
using Microsoft.EntityFrameworkCore;

namespace GardenService.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class MoistureController : ControllerBase
    {

        private readonly GardenServicesDbContext? _gardenDbContext;
        private readonly ILogger<MoistureController>? _logger;

        public MoistureController(ILogger<MoistureController> logger, GardenServicesDbContext gardenServicesDbContext)
        {
            this._logger = logger;
            this._gardenDbContext = gardenServicesDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Moisture>>> GetMoistures()
        {
            return Ok(await _gardenDbContext!.Moistures.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Moisture>> GetMoisure(int id)
        {
            var moisture = await _gardenDbContext!.Moistures.FindAsync(id);

            if (moisture == null)
            {
                return NotFound();
            }

            return Ok(moisture);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoisture(int id, Moisture moisture)
        {
            if (id != moisture.IX_Moisture)
            {
                return BadRequest();
            }

            _gardenDbContext!.Entry(moisture).State = EntityState.Modified;

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
        public async Task<ActionResult<Moisture>> PostMoisture(Moisture moisture)
        {
            _gardenDbContext!.Moistures.Add(moisture);
            await _gardenDbContext!.SaveChangesAsync();

            return CreatedAtAction("GetMoisture", new { id = moisture.IX_Moisture }, moisture);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Moisture>> DeleteMoisture(int id)
        {
            var moisture = await _gardenDbContext!.Moistures.FindAsync(id);
            if (moisture == null)
            {
                return NotFound();
            }

            _gardenDbContext!.Moistures.Remove(moisture);
            await _gardenDbContext!.SaveChangesAsync();

            return Ok(moisture);
        }

        private bool IsOrderExists(int id)
        {
            return _gardenDbContext!.Moistures.Any(e => e.IX_Moisture == id);
        }

    }
}