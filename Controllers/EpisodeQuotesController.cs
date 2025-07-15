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

        /// <summary>
        /// Gets all quotes from all episodes.
        /// </summary>
        /// <returns>A list of all episode quotes across the series.</returns>
        [HttpGet]
        public ActionResult<List<QuoteCreateDto>> GetAllQuotes()
        {
            // Get all quotes in the system
            List<QuoteCreateDto> quotes = _quotesService.GetAllQuotes();

            // Return them with 200 OK
            return Ok(quotes);
        }

        /// <summary>
        /// Gets all quotes for a specific episode.
        /// </summary>
        /// <param name="episodeId">The ID of the episode.</param>
        /// <returns>A list of quotes tied to the specified episode.</returns>
        [HttpGet("episode/{episodeId}")]
        public ActionResult<List<QuoteCreateDto>> GetQuotesForEpisode(int episodeId)
        {
            // Get all quotes for a specific episode
            List<QuoteCreateDto> quotes = _quotesService.GetQuotesForEpisode(episodeId);

            // Return them with 200 OK
            return Ok(quotes);
        }


        /// <summary>
        /// Gets a single quote by its ID.
        /// </summary>
        /// <param name="id">The ID of the quote to retrieve.</param>
        /// <returns>The quote data, or 404 if not found.</returns>
        [HttpGet("{id}")]
        public ActionResult<QuoteCreateDto> GetQuoteById(int id)
        {
            // Try to get the quote with this ID
            QuoteCreateDto quote = _quotesService.GetQuoteById(id);

            // If not found, return 404
            if (quote == null)
                return NotFound();

            // Return the quote with 200 OK
            return Ok(quote);
        }

        /// <summary>
        /// Adds a new quote and links it to an episode.
        /// </summary>
        /// <param name="quote">The quote data to add.</param>
        /// <returns>201 Created if successful, or 400 if the quote could not be added.</returns>
        [HttpPost]
        public ActionResult AddQuote([FromBody] QuoteCreateDto quote)
        {
            // Try to add the quote (only if episode exists)
            bool success = _quotesService.AddQuote(quote);

            // Return 400 if episode doesn't exist or add fails
            if (!success)
                return BadRequest("Quote could not be added. Make sure the episode exists.");

            // Return 201 Created with location of new quote
            return CreatedAtAction(nameof(GetQuoteById), new { id = quote.Id }, quote);
        }

        /// <summary>
        /// Updates an existing quote.
        /// </summary>
        /// <param name="id">The ID of the quote to update.</param>
        /// <param name="quote">The updated quote data.</param>
        /// <returns>204 No Content if successful, 400 if the ID is mismatched, or 404 if not found.</returns>
        [HttpPut("{id}")]
        public ActionResult UpdateQuote(int id, [FromBody] QuoteUpdateDto quote)
        {

            // Try to update the quote
            bool success = _quotesService.UpdateQuote(id, quote);

            // If update failed (e.g. quote doesn’t exist), return 404
            if (!success)
                return NotFound();

            // Return 204 No Content to indicate successful update
            return NoContent();
        }

        /// <summary>
        /// Deletes a quote by ID.
        /// </summary>
        /// <param name="id">The ID of the quote to delete.</param>
        /// <returns>204 No Content if deleted, or 404 if not found.</returns>
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