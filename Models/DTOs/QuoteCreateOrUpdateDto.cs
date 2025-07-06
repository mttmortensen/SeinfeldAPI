using System.ComponentModel.DataAnnotations;

namespace SeinfeldAPI.Models.DTOs
{
    /*
     * POST /api/episodequotes
     * 
     * PUT /api/episodequotes
     * 
     * This Dto is to be used when Adding (POST) or
     * Updating (PUT) a new EpisodeQuote. By using 
     * this Dto, we can still allow the relation 
     * between Episode and EpisodeQuote to happen.
     */
    public class QuoteCreateOrUpdateDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 1)]
        public string Quote { get; set; }

        [Required]
        [StringLength(100)]
        public string Character { get; set; }

        // Already validated manually in service layer
        // via ResolveEpisodeId()
        public int? EpisodeId { get; set; }

        [StringLength(100)]
        public string? EpisodeTitle { get; set; }

        [StringLength(20)]
        public string? EpisodeSeason { get; set; }
    }
}
