using SeinfeldAPI.Services.Attributes;
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

        /// <summary>
        /// The episode title must start with "The ", and be between 8–21 characters.
        /// </summary>
        [ValidSeinfeldTitle]
        public string? Title { get; set; }

        /// <summary>
        /// Season must be in the format 'S1' to 'S9' with no leading zeros.
        /// </summary>
        [ValidSeasonOrEpisodeNumber(ValidSeasonOrEpisodeNumber.ValidationType.Season)]
        public string? Season { get; set; }

        /// <summary>
        /// EpisodeNumber must be in the format 'E1' to 'E30' with no leading zeros.
        /// </summary>
        [ValidSeasonOrEpisodeNumber(ValidSeasonOrEpisodeNumber.ValidationType.EpisodeNumber)]
        public string? EpisodeNumber { get; set; }

        public DateTime? AirDate { get; set; }
    }
}
