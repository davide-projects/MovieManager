using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MovieManager.BLL.Models;
using MovieManager.BLL.Services.Interfaces;
using MovieManager.DAL.Entities;

namespace MovieManager.PL.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DirectorsController : ControllerBase
    {
        private readonly IGenericService<DirectorModel> _directorService;

        public DirectorsController (IGenericService<DirectorModel> directorService)
        {
            _directorService = directorService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DirectorModel>>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var director = await _directorService.GetAllAsync(cancellationToken);
            return Ok(director);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task <ActionResult<DirectorModel>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var director = await _directorService.GetByIdAsync(id, cancellationToken);
            if (director == null)
            {
                return NotFound();
            }
            return Ok(director);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DirectorModel>> CreateAsync([FromBody] DirectorModel model, CancellationToken cancellationToken = default)
        {
            var created = await _directorService.CreateAsync(model, cancellationToken);
            return CreatedAtAction(nameof(GetByIdAsync), new {id = created.Id}, created);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] DirectorModel model ,CancellationToken cancellationToken = default)
        {
            if (model.Id != id)
            {
                return BadRequest($"Route id ({id}) and body id ({model.Id}) must match.");
            }
            var updated = await _directorService.UpdateAsync(model, cancellationToken);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var deleted = await _directorService.DeleteAsync(id, cancellationToken);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
