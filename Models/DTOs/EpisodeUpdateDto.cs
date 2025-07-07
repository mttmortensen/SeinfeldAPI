using System.ComponentModel.DataAnnotations;

namespace SeinfeldAPI.Models.DTOs
{
    /*
     * PUT /api/episodes
     * 
     * This is only being used in PUT for Epsiodes 
     * As I did not want to also include updating quotes here 
     * As this would break the service layer for EpisodeQuotes
     * This keeps it seperate
     */

    public class EpisodeUpdateDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        [RegularExpression(@"^S[1-9]$", ErrorMessage = "Season must be in the format 'S1' to 'S9' with no leading zeros.")]
        public string? Season { get; set; }

        [RegularExpression(@"^E([1-9]|[1-2][0-9]|30)$", ErrorMessage = "EpisodeNumber must be in the format 'E1' to 'E30' with no leading zeros.")]
        public string? EpisodeNumber { get; set; }
        public DateTime? AirDate { get; set; }
    }
}
