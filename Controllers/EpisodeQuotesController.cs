using Microsoft.AspNetCore.Mvc;
using SeinfeldAPI.Models;
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

        // Handles GET requests to /api/episodequotes
        // This brings in all quotes from the db 
        [HttpGet]
        public ActionResult<List<EpisodeQuotes>> GetAllQuotes()
        {
            // Get all quotes in the system
            List<EpisodeQuotes> quotes = _quotesService.GetAllQuotes();

            // Return them with 200 OK
            return Ok(quotes);
        }

        // Handles GET requests to /api/episodequotes/episode/{episodeId}
        // This brings in all the quotes for a specific episode
        [HttpGet("episode/{episodeId}")]
        public ActionResult<List<EpisodeQuotes>> GetQuotesForEpisode(int episodeId)
        {
            // Get all quotes for a specific episode
            List<EpisodeQuotes> quotes = _quotesService.GetQuotesForEpisode(episodeId);

            // Return them with 200 OK
            return Ok(quotes);
        }

        // Handles GET requests to /api/episodequotes/{id}
        [HttpGet("{id}")]
        public ActionResult<EpisodeQuotes> GetQuoteById(int id)
        {
            // Try to get the quote with this ID
            EpisodeQuotes quote = _quotesService.GetQuoteById(id);

            // If not found, return 404
            if (quote == null)
                return NotFound();

            // Return the quote with 200 OK
            return Ok(quote);
        }
    }
}