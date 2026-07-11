using Microsoft.AspNetCore.Mvc;
using MovieManager.BLL.Models;
using MovieManager.BLL.Services.Interfaces;

namespace MovieManager.PL.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ActorsController : ControllerBase
    {
        private readonly IGenericService<ActorModel> _actorService;

        public ActorsController(IGenericService<ActorModel> actorService)
        {
            _actorService = actorService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ActorModel>>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var actors = await _actorService.GetAllAsync(cancellationToken);
            return Ok(actors);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ActorModel>> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var actor = await _actorService.GetByIdAsync(id, cancellationToken);
            if (actor == null)
            {
                return NotFound();
            }
            return Ok(actor);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ActorModel>> CreateAsync(
            [FromBody] ActorModel model,
            CancellationToken cancellationToken = default)
        {
            var created = await _actorService.CreateAsync(model, cancellationToken);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(
            int id,
            [FromBody] ActorModel model,
            CancellationToken cancellationToken = default)
        {
            if (model.Id != id)
            {
                return BadRequest($"Route id ({id}) and body id ({model.Id}) must match.");
            }

            var updated = await _actorService.UpdateAsync(model, cancellationToken);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var deleted = await _actorService.DeleteAsync(id, cancellationToken);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
