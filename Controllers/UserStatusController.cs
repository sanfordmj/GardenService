using Microsoft.AspNetCore.Mvc;
using GardenService.Models;
using Microsoft.EntityFrameworkCore;

namespace GardenService.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserStatusController : ControllerBase
    {

        private readonly GardenServicesDbContext? _gardenDbContext;
        private readonly ILogger<UserStatusController>? _logger;

        public UserStatusController(ILogger<UserStatusController> logger, GardenServicesDbContext gardenServicesDbContext)
        {
            this._logger = logger;
            this._gardenDbContext = gardenServicesDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserStatus>>> GetUserStatus()
        {
            return Ok(await _gardenDbContext!.UserStatus.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserStatus>> GetUserStatus(int id)
        {
            var reading = await _gardenDbContext!.UserStatus.FindAsync(id);

            if (reading == null)
            {
                return NotFound();
            }

            return Ok(reading);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserStatus(int id, UserStatus model)
        {
            if (id != model.IX_UserStatus)
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
                if (!IsUserStatusExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<SensorType>> PostUserStatus(UserStatus model)
        {
            _gardenDbContext!.UserStatus.Add(model);
            await _gardenDbContext!.SaveChangesAsync();

            return CreatedAtAction("GetUserStatus", new { id = model.IX_UserStatus }, model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserStatus>> DeleteUserStatus(int id)
        {
            var model = await _gardenDbContext!.UserStatus.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _gardenDbContext!.UserStatus.Remove(model);
            await _gardenDbContext!.SaveChangesAsync();

            return Ok(model);
        }

        private bool IsUserStatusExists(int id)
        {
            return _gardenDbContext!.UserStatus.Any(e => e.IX_UserStatus == id);
        }

    }
}