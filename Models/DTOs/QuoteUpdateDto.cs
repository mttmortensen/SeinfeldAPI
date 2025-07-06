using System.ComponentModel.DataAnnotations;

namespace SeinfeldAPI.Models.DTOs
{
    /*
     * PUT /api/episodequotes
     * 
     * This Dto is to be used when 
     * Updating an EpisodeQuote.
     * By using this Dto, we can allow for optional values
     * So that not each value needs to be updated
     */

    public class QuoteUpdateDto
    {
        public int Id { get; set; }

        [StringLength(300)]
        public string? Quote { get; set; }

        [StringLength(100)]
        public string? Character { get; set; }

        public int? EpisodeId { get; set; }

        [StringLength(100)]
        public string? EpisodeTitle { get; set; }

        [StringLength(20)]
        public string? EpisodeSeason { get; set; }
    }

}
