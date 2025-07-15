using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SeinfeldAPI.Interfaces;
using SeinfeldAPI.Models.DTOs;

namespace SeinfeldAPI.Controllers
{
    // Can now use jwt here with username and password
    [Authorize]

    // I'll be doing RateLimiting at the Controller level
    // If this scales out then I can apply this down to more specific routes
    [EnableRateLimiting("fixed")]

    // Tells ASP.NET this class is an API controller and enables automatic model validation, binding, etc.
    [ApiController]

    // Sets the route to "api/episodes" 
    [Route("api/episodes")]
    public class EpisodeController : ControllerBase
    {
        // Service that contains logic for managing episodes
        private readonly IEpisodeService _episodeService;

        // Constructor injection to get the EpisodeService instance
        public EpisodeController(IEpisodeService episodeService)
        {
            _episodeService = episodeService;
        }

        /// <summary>
        /// Gets all episodes with their associated quotes.
        /// </summary>
        /// <returns>A list of episodes with quote data.</returns>
        [HttpGet]
        public ActionResult<List<EpisodeWithQuotesDto>> GetAllEpisodes()
        {
            // Fetches all episodes from the service
            List<EpisodeWithQuotesDto> episodes = _episodeService.GetAllEpisodes();

            // Return 200 OK with the list of episodes
            return Ok(episodes);
        }

        /// <summary>
        /// Gets a single episode by ID.
        /// </summary>
        /// <param name="id">The ID of the episode.</param>
        /// <returns>The episode with quote data, or 404 if not found.</returns>
        [HttpGet("{id}")]
        public ActionResult<EpisodeWithQuotesDto> GetEpisodeById(int id)
        {
            // Try to get the episode by it's Id
            EpisodeWithQuotesDto episode = _episodeService.GetEpisodeById(id);

            // If no match, return 404
            if (episode == null)
                return NotFound();

            // Otherwise return the episode with 200
            return Ok(episode);
        }

        /// <summary>
        /// Creates a new episode with optional quotes.
        /// </summary>
        /// <param name="episodeDto">The episode data to create.</param>
        /// <returns>The created episode, or 400 if creation failed.</returns>
        [HttpPost]
        public ActionResult AddEpisode([FromBody] EpisodeWithQuotesDto episodeDto)
        {
            var created = _episodeService.AddEpisode(episodeDto);

            if (created == null)
                return BadRequest("Episode could not be added");

            return CreatedAtAction(nameof(GetEpisodeById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing episode.
        /// </summary>
        /// <param name="id">The ID of the episode to update.</param>
        /// <param name="episode">The updated episode data.</param>
        /// <returns>204 No Content if successful, 400 or 404 otherwise.</returns>
        [HttpPut("{id}")]
        public ActionResult UpdateEpisode(int id, [FromBody] EpisodeUpdateDto episode) 
        {
            // Return 400 if Ids don't match
            if (id != episode.Id)
                return BadRequest("Id in URL doesn't match Id in body");

            // Try to update the episode 
            bool success = _episodeService.UpdateEpisode(episode);

            // If update failed return 404
            if (!success)
                return NotFound();

            // Return 204 No Content to indicate sucess with no return body
            return NoContent();
        }

        /// <summary>
        /// Deletes an episode by ID.
        /// </summary>
        /// <param name="id">The ID of the episode to delete.</param>
        /// <returns>204 No Content if deleted, 404 if not found.</returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteEpisode(int id)
        {
            // Try to delete the episode
            var success = _episodeService.DeleteEpisode(id);

            // Return 404 if it didn’t exist
            if (!success)
                return NotFound();

            // Return 204 No Content to confirm deletion
            return NoContent();
        }
    }
}
