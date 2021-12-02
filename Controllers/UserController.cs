using Microsoft.AspNetCore.Mvc;
using GardenService.Models;
using Microsoft.EntityFrameworkCore;

namespace GardenService.Controllers
{
    [ApiController]
    [Route("[controller]")]   
    public class UserController : ControllerBase
    {
        private readonly GardenServicesDbContext? _gardenDbContext;
        private readonly ILogger<UserController>? _logger;

        public UserController(ILogger<UserController> logger, GardenServicesDbContext gardenServicesDbContext)
        {
            this._logger = logger;
            this._gardenDbContext = gardenServicesDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _gardenDbContext!.Users.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var reading = await _gardenDbContext!.Users.FindAsync(id);

            if (reading == null)
            {
                return NotFound();
            }

            return Ok(reading);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User model)
        {
            if (id != model.IX_User)
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
        public async Task<ActionResult<User>> PostSensorReading(User model)
        {
            _gardenDbContext!.Users.Add(model);
            await _gardenDbContext!.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = model.IX_User }, model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteSensorReading(int id)
        {
            var model = await _gardenDbContext!.Users.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _gardenDbContext!.Users.Remove(model);
            await _gardenDbContext!.SaveChangesAsync();

            return Ok(model);
        }

        private bool IsOrderExists(int id)
        {
            return _gardenDbContext!.Users.Any(e => e.IX_User == id);
        }

    }
}