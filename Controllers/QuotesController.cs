using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SeinfeldAPI.Interfaces;
using SeinfeldAPI.Models;
using SeinfeldAPI.Models.DTOs;

namespace SeinfeldAPI.Controllers
{
    /*
     * Endpoints for the quote entry frontend (SeinfeldEntryQuoter).
     *
     * Kept separate from /api/episodequotes so that the existing
     * contract stays the same. Quotes here are added by
     * Season + EpisodeNumber, and any speaker is allowed.
     */

    // Can now use jwt here with username and password
    [Authorize]

    // Same rate limiting as the other controllers
    [EnableRateLimiting("fixed")]

    [ApiController]

    // Sets base route to "api/quotes"
    [Route("api/quotes")]
    public class QuotesController : ControllerBase
    {
        private readonly IEpisodeQuotesService _quotesService;

        public QuotesController(IEpisodeQuotesService quotesService)
        {
            _quotesService = quotesService;
        }

        /// <summary>
        /// Gets the most recently added quotes, newest first.
        /// </summary>
        /// <param name="limit">How many quotes to return (1–50, default 5).</param>
        /// <returns>A list of recent quotes with their episode info.</returns>
        [HttpGet("recent")]
        public ActionResult<List<QuoteEntryDto>> GetRecentQuotes([FromQuery] int limit = 5)
        {
            if (limit < 1 || limit > 50)
                return BadRequest(new { message = "Limit must be between 1 and 50." });

            return Ok(_quotesService.GetRecentQuotes(limit));
        }

        /// <summary>
        /// Adds a quote to an episode by season and episode number.
        /// If the episode doesn't exist, it is created with EpisodeTitle.
        /// </summary>
        /// <param name="quote">The quote, speaker, season, episode number and optional episode title.</param>
        /// <returns>
        /// 201 Created with the new quote.
        /// 400 Bad Request if the episode is new and no title was given.
        /// 409 Conflict if this quote is already saved for the episode.
        /// </returns>
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        [ProducesResponseType(typeof(QuoteEntryDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status409Conflict)]
        public ActionResult AddQuote([FromBody] QuoteEntryCreateDto quote)
        {
            var (status, created) = _quotesService.AddQuoteEntry(quote);

            if (status == QuoteEntryStatus.EpisodeTitleRequired)
                return BadRequest(new { message = "This episode is new. Add an episode title." });

            if (status == QuoteEntryStatus.Duplicate)
                return Conflict(new { message = "That quote is already saved for this episode." });

            return CreatedAtAction(nameof(EpisodeQuotesController.GetQuoteById), "EpisodeQuotes", new { id = created!.Id }, created);
        }

        /// <summary>
        /// Updates the text and speaker of a quote.
        /// </summary>
        /// <param name="id">The ID of the quote to update.</param>
        /// <param name="quote">The new text and speaker.</param>
        /// <returns>204 No Content if successful, 404 if not found, 409 if it would duplicate another quote.</returns>
        [Authorize(Roles = Roles.Admin)]
        [HttpPut("{id}")]
        public ActionResult UpdateQuote(int id, [FromBody] QuoteEntryUpdateDto quote)
        {
            QuoteEntryStatus status = _quotesService.UpdateQuoteEntry(id, quote);

            if (status == QuoteEntryStatus.NotFound)
                return NotFound();

            if (status == QuoteEntryStatus.Duplicate)
                return Conflict(new { message = "That quote is already saved for this episode." });

            return NoContent();
        }

        /// <summary>
        /// Deletes a quote by ID.
        /// </summary>
        /// <param name="id">The ID of the quote to delete.</param>
        /// <returns>204 No Content if deleted, or 404 if not found.</returns>
        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id}")]
        public ActionResult DeleteQuote(int id)
        {
            // Returns false when the quote doesn't exist
            if (!_quotesService.DeleteQuote(id))
                return NotFound();

            return NoContent();
        }
    }
}
