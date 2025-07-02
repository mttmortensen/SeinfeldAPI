using Microsoft.AspNetCore.Mvc;
using SeinfeldAPI.Interfaces;
using SeinfeldAPI.Models.DTOs;

namespace SeinfeldAPI.Controllers
{
    // Marks this class as an API controller
    [ApiController]

    // Sets base route to "api/episodequotes"
    [Route("api/episodequotes")]
    public class EpisodeQuotesController : ControllerBase
    {
        // Service that handles all quote logic
        private readonly IEpisodeQuotesService _quotesService;

        // Inject the EpisodeQuotesService through constructor
        public EpisodeQuotesController(IEpisodeQuotesService quotesService)
        {
            _quotesService = quotesService;
        }

        // Handles GET requests to /api/episodequotes
        // This brings in all quotes from the db 
        [HttpGet]
        public ActionResult<List<EpisodeQuoteDto>> GetAllQuotes()
        {
            // Get all quotes in the system
            List<EpisodeQuoteDto> quotes = _quotesService.GetAllQuotes();

            // Return them with 200 OK
            return Ok(quotes);
        }

        // Handles GET requests to /api/episodequotes/episode/{episodeId}
        // This brings in all the quotes for a specific episode
        [HttpGet("episode/{episodeId}")]
        public ActionResult<List<EpisodeQuoteDto>> GetQuotesForEpisode(int episodeId)
        {
            // Get all quotes for a specific episode
            List<EpisodeQuoteDto> quotes = _quotesService.GetQuotesForEpisode(episodeId);

            // Return them with 200 OK
            return Ok(quotes);
        }

        // Handles GET requests to /api/episodequotes/{id}
        [HttpGet("{id}")]
        public ActionResult<EpisodeQuoteDto> GetQuoteById(int id)
        {
            // Try to get the quote with this ID
            EpisodeQuoteDto quote = _quotesService.GetQuoteById(id);

            // If not found, return 404
            if (quote == null)
                return NotFound();

            // Return the quote with 200 OK
            return Ok(quote);
        }

        // Handles POST requests to add a new quote and will add to the correct episode 
        // thanks to service layer
        [HttpPost]
        public ActionResult AddQuote([FromBody] EpisodeQuoteDto quote)
        {
            // Try to add the quote (only if episode exists)
            bool success = _quotesService.AddQuote(quote);

            // Return 400 if episode doesn't exist or add fails
            if (!success)
                return BadRequest("Quote could not be added. Make sure the episode exists.");

            // Return 201 Created with location of new quote
            return CreatedAtAction(nameof(GetQuoteById), new { id = quote.Id }, quote);
        }

        // Handles PUT requests to update a quote by ID
        [HttpPut("{id}")]
        public ActionResult UpdateQuote(int id, [FromBody] EpisodeQuoteDto quote)
        {
            // If the ID in URL doesn't match the one in body, reject it
            if (id != quote.Id)
                return BadRequest("ID in URL doesn't match ID in body.");

            // Try to update the quote
            bool success = _quotesService.UpdateQuote(quote);

            // If update failed (e.g. quote doesn’t exist), return 404
            if (!success)
                return NotFound();

            // Return 204 No Content to indicate successful update
            return NoContent();
        }

        // Handles DELETE requests to remove a quote by ID
        [HttpDelete("{id}")]
        public ActionResult DeleteQuote(int id)
        {
            // Try to delete the quote
            var success = _quotesService.DeleteQuote(id);

            // Return 404 if quote was not found
            if (!success)
                return NotFound();

            // Return 204 No Content to confirm deletion
            return NoContent();
        }
    }
}