using Microsoft.AspNetCore.Mvc;
using SeinfeldAPI.Interfaces;
using SeinfeldAPI.Models.DTOs;

namespace SeinfeldAPI.Controllers
{
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

        // Handles GET requests to /api/epsiodes
        [HttpGet]
        public ActionResult<List<EpisodeDto>> GetAllEpisodes()
        {
            // Fetches all episodes from the service
            List<EpisodeDto> episodes = _episodeService.GetAllEpisodes();

            // Return 200 OK with the list of episodes
            return Ok(episodes);
        }

        // Handles GET requests to /api/episodes/{id}
        [HttpGet("{id}")]
        public ActionResult<EpisodeDto> GetEpisodeById(int id)
        {
            // Try to get the episode by it's Id
            EpisodeDto episode = _episodeService.GetEpisodeById(id);

            // If no match, return 404
            if (episode == null)
                return NotFound();

            // Otherwise return the episode with 200
            return Ok(episode);
        }

        // Handles POST requests to /api/episodes for a new episode
        [HttpPost]
        public ActionResult AddEpisode([FromBody] EpisodeDto episodeDto)
        {
            var created = _episodeService.AddEpisode(episodeDto);

            if (created == null)
                return BadRequest("Episode could not be added");

            return CreatedAtAction(nameof(GetEpisodeById), new { id = created.Id }, created);
        }

        // Handles PUT request to to update an existing episode
        [HttpPut("{id}")]
        public ActionResult UpdateEpisode(int id, [FromBody] EpisodeFlatDto episode) 
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

        // Handles DELETE requests to remove an episode
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
