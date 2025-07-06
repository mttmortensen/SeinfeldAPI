using System.ComponentModel.DataAnnotations;

namespace SeinfeldAPI.Models.DTOs
{
    /*
     * GET /api/episodes
     * 
     * POST /api/episodes
     * 
     * This Dto is used in GET Requests under Episodes
     * When a user goes to /api/episodes, the quotes block 
     * will pull this Dto as I did not want to have repeated
     * Episode info such as Title and Season if it was already
     * being listed in the parent.
     * 
     * This will also be used in the POST method for creating an Episode
     * As doing EpisodeQuoteDto would require to place the title and season again
     */
    public class QuoteInlineDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 1)]
        public string Quote { get; set; }

        [Required]
        [StringLength(100)]
        public string Character { get; set; }

        public int EpisodeId { get; set; }
    }
}
