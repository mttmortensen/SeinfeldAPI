using System.ComponentModel.DataAnnotations;
using SeinfeldAPI.Services.Attributes;

namespace SeinfeldAPI.Models.DTOs
{
    /*
     * This Dto is used for any GET or POST request logic 
     * The QuoteInlineDto is to help with the nasty rat's nest with GET reqs
     * And it allows for simple adding of quotes in POST reqs 
     * All is needed for adding a quote within a episode is
     * The Quote itself and who said it
     */
    public class EpisodeWithQuotesDto
    {

        public int Id { get; set; }

        /// <summary>
        /// The episode title must start with "The ", and be between 8–21 characters.
        /// </summary>
        [ValidSeinfeldTitle]
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Season must be in the format 'S1' to 'S9' with no leading zeros.
        /// </summary>
        [ValidSeasonOrEpisodeNumber(ValidSeasonOrEpisodeNumber.ValidationType.Season)]
        [Required]
        public string Season { get; set; }

        /// <summary>
        /// EpisodeNumber must be in the format 'E1' to 'E30' with no leading zeros.
        /// </summary>
        [ValidSeasonOrEpisodeNumber(ValidSeasonOrEpisodeNumber.ValidationType.EpisodeNumber)]
        [Required]
        public string EpisodeNumber { get; set; }

        [Required]
        public DateTime AirDate { get; set; }

        // Including now the simplifed EpisodeQuotes data
        public List<QuoteInlineDto> Quotes { get; set; }
    }
}
