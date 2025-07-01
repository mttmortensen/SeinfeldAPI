using Microsoft.AspNetCore.Mvc;
using SeinfeldAPI.Services;

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
    }
}
