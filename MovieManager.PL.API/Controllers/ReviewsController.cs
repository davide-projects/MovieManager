using Microsoft.AspNetCore.Mvc;
using MovieManager.BLL.Models;
using MovieManager.BLL.Services.Interfaces;

namespace MovieManager.PL.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ReviewsController : ControllerBase
    {
        private readonly IGenericService<ReviewModel> _reviewsService;

        public ReviewsController(IGenericService<ReviewModel> reviewsService)
        {
            _reviewsService = reviewsService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ReviewModel>>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var reviews = await _reviewsService.GetAllAsync(cancellationToken);
            return Ok(reviews);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReviewModel>> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var review = await _reviewsService.GetByIdAsync(id, cancellationToken);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReviewModel>> CreateAsync(
            [FromBody] ReviewModel model,
            CancellationToken cancellationToken = default)
        {
            var created = await _reviewsService.CreateAsync(model, cancellationToken);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(
            int id,
            [FromBody] ReviewModel model,
            CancellationToken cancellationToken = default)
        {
            if (model.Id != id)
            {
                return BadRequest($"Route id ({id}) and body id ({model.Id}) must match.");
            }

            var updated = await _reviewsService.UpdateAsync(model, cancellationToken);
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
            var deleted = await _reviewsService.DeleteAsync(id, cancellationToken);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
