using System.ComponentModel.DataAnnotations;

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

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [RegularExpression(@"^S[1-9]$", ErrorMessage = "Season must be in the format 'S1' to 'S9' with no leading zeros.")]
        public string Season { get; set; }

        [Required]
        [RegularExpression(@"^E([1-9]|[1-2][0-9]|30)$", ErrorMessage = "EpisodeNumber must be in the format 'E1' to 'E30' with no leading zeros.")]
        public string EpisodeNumber { get; set; }

        [Required]
        public DateTime AirDate { get; set; }

        // Including now the simplifed EpisodeQuotes data
        public List<QuoteInlineDto> Quotes { get; set; }
    }
}
