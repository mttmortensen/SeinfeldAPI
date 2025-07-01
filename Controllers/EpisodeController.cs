using Microsoft.AspNetCore.Mvc;
using SeinfeldAPI.Services;
using SeinfeldAPI.Models;

namespace SeinfeldAPI.Controllers
{
    // Tells ASP.NET this class is an API controller and enables automatic model validation, binding, etc.
    [ApiController]

    // Sets the route to "api/episodes" 
    [Route("api/episodes")]
    public class EpisodeController : ControllerBase
    {
        // Service that contains logic for managing episodes
        private readonly EpisodeService _episodeService;

        // Constructor injection to get the EpisodeService instance
        public EpisodeController(EpisodeService episodeService)
        {
            _episodeService = episodeService;
        }

        // Handles GET requests to /api/epsiodes
        [HttpGet]
        public ActionResult<List<Episode>> GetAllEpisodes() 
        {
            // Fetches all episodes from the service
            List<Episode> episodes = _episodeService.GetAllEpisodes();

            // Return 200 OK with the list of episodes
            return Ok(episodes);
        }

        // Handles GET requests to /api/episodes/{id}
        [HttpGet("{id}")]
        public ActionResult<Episode> GetEpisodeById(int id) 
        {
            // Try to get the episode by it's Id
            Episode episode = _episodeService.GetEpisodeById(id);

            // If no match, return 404
            if (episode == null)
                return NotFound();

            // Otherwise return the episode with 200
            return Ok(episode);
        }
    }
}
