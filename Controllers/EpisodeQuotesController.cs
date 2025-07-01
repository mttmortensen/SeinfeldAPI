using Microsoft.AspNetCore.Mvc;
using SeinfeldAPI.Services;

namespace SeinfeldAPI.Controllers
{
    // Marks this class as an API controller
    [ApiController]

    // Sets base route to "api/episodequotes"
    [Route("api/episodequotes")]
    public class EpisodeQuotesController : ControllerBase
    {
        // Service that handles all quote logic
        private readonly EpisodeQuotesService _quotesService;

        // Inject the EpisodeQuotesService through constructor
        public EpisodeQuotesController(EpisodeQuotesService quotesService)
        {
            _quotesService = quotesService;
        }
    }
}