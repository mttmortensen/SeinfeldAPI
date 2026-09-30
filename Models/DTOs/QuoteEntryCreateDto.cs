using System.ComponentModel.DataAnnotations;

namespace SeinfeldAPI.Models.DTOs
{
    /*
     * POST /api/quotes
     *
     * This Dto is used by the quote entry frontend.
     * The episode is found by Season + EpisodeNumber.
     * If that episode doesn't exist yet, it gets created with EpisodeTitle.
     * If it does exist, EpisodeTitle is ignored.
     *
     * Speaker is not limited to the main 4 characters here,
     * since Newman, Puddy, etc. say plenty of quotable things.
     */
    public class QuoteEntryCreateDto
    {
        [Required]
        [StringLength(1000)]
        public string Text { get; set; }

        [Required]
        [StringLength(100)]
        public string Speaker { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Season must be 1 or higher.")]
        public int Season { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Episode must be 1 or higher.")]
        public int EpisodeNumber { get; set; }

        // Only required when the episode doesn't exist yet (checked in service layer)
        [StringLength(200)]
        public string? EpisodeTitle { get; set; }
    }
}
