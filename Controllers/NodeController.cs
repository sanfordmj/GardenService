using Microsoft.AspNetCore.Mvc;
using GardenService.Models;
using Microsoft.EntityFrameworkCore;

namespace GardenService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NodeController : ControllerBase
    {
        private readonly GardenServicesDbContext? _gardenDbContext;
        private readonly ILogger<NodeController>? _logger;

        public NodeController(ILogger<NodeController> logger, GardenServicesDbContext gardenServicesDbContext)
        {
            this._logger = logger;
            this._gardenDbContext = gardenServicesDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Node>>> GetUsers()
        {
            return Ok(await _gardenDbContext!.Nodes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Node>> GetNode(int id)
        {
            var reading = await _gardenDbContext!.Nodes.FindAsync(id);

            if (reading == null)
            {
                return NotFound();
            }

            return Ok(reading);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutNode(int id, Node model)
        {
            if (id != model.IX_Node)
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
                if (!IsNodeExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<Node>> PostNode(Node model)
        {
            _gardenDbContext!.Nodes.Add(model);
            await _gardenDbContext!.SaveChangesAsync();

            return CreatedAtAction("GetNode", new { id = model.IX_Node }, model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteNode(int id)
        {
            var model = await _gardenDbContext!.Nodes.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _gardenDbContext!.Nodes.Remove(model);
            await _gardenDbContext!.SaveChangesAsync();

            return Ok(model);
        }

        private bool IsNodeExists(int id)
        {
            return _gardenDbContext!.Nodes.Any(e => e.IX_Node == id);
        }

    }
}