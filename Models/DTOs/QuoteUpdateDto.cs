using SeinfeldAPI.Interfaces;
using SeinfeldAPI.Services.Attributes;
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

    public class QuoteUpdateDto : IEpisodeResolvable
    {

        [StringLength(300)]
        public string? Quote { get; set; }

        /// <summary>
        /// Character must be part of the original 4 characters: Jerry, George, Elanie, and Kramer.
        /// </summary>
        [ValidMainCharacter]
        public string? Character { get; set; }

        public int? EpisodeId { get; set; }

        /// <summary>
        /// The episode title must start with "The ", and be between 8–21 characters.
        /// </summary>
        [ValidSeinfeldTitle]
        public string? EpisodeTitle { get; set; }


        public string? EpisodeSeason { get; set; }
    }

}
