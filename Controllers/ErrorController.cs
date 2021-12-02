using Microsoft.AspNetCore.Mvc;
using GardenService.Models;
using Microsoft.EntityFrameworkCore;

namespace GardenService.Controllers
{

    [ApiController]
    [Route("[controller]")]   
    public class ErrorController : ControllerBase
    {

        private readonly GardenServicesDbContext? _gardenDbContext;
        private readonly ILogger<ErrorController>? _logger;

        public ErrorController(ILogger<ErrorController> logger, GardenServicesDbContext gardenServicesDbContext)
        {
            this._logger = logger;
            this._gardenDbContext = gardenServicesDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Error>>> GetErrors()
        {
            return Ok(await _gardenDbContext!.Errors.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Error>> GetError(int id)
        {
            var reading = await _gardenDbContext!.Errors.FindAsync(id);

            if (reading == null)
            {
                return NotFound();
            }

            return Ok(reading);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutError(int id, Error model)
        {
            if (id != model.IX_Error)
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
        public async Task<ActionResult<Error>> PostSensorReading(Error model)
        {
            _gardenDbContext!.Errors.Add(model);
            await _gardenDbContext!.SaveChangesAsync();

            return CreatedAtAction("GetError", new { id = model.IX_Error }, model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Error>> DeleteSensorReading(int id)
        {
            var model = await _gardenDbContext!.Errors.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _gardenDbContext!.Errors.Remove(model);
            await _gardenDbContext!.SaveChangesAsync();

            return Ok(model);
        }

        private bool IsOrderExists(int id)
        {
            return _gardenDbContext!.Errors.Any(e => e.IX_Error == id);
        }

    }
}