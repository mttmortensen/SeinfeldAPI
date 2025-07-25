﻿using SeinfeldAPI.Interfaces;
using SeinfeldAPI.Services.Attributes;
using System.ComponentModel.DataAnnotations;

namespace SeinfeldAPI.Models.DTOs
{
    /*
     * POST /api/episodequotes
     * 
     * This Dto is to be used when 
     * Adding (POST) a new EpisodeQuote.
     * By using this Dto, we can still allow the relation 
     * between Episode and EpisodeQuote to happen.
     */
    public class QuoteCreateDto : IEpisodeResolvable
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 1)]
        public string Quote { get; set; }

        /// <summary>
        /// Character must be part of the original 4 characters: Jerry, George, Elanie, and Kramer.
        /// </summary>
        [ValidMainCharacter]
        [Required]
        public string Character { get; set; }

        // Already validated manually in service layer
        // via ResolveEpisodeId()
        public int? EpisodeId { get; set; }

        /// <summary>
        /// The episode title must start with "The ", and be between 8–21 characters.
        /// </summary>
        [ValidSeinfeldTitle]
        [Required]
        public string? EpisodeTitle { get; set; }

        /// <summary>
        /// Season must be in the format 'S1' to 'S9' with no leading zeros.
        /// </summary>
        [ValidSeasonOrEpisodeNumber(ValidSeasonOrEpisodeNumber.ValidationType.Season)]
        [Required]
        public string? EpisodeSeason { get; set; }
    }
}
