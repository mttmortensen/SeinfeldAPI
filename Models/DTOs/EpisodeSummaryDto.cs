namespace SeinfeldAPI.Models.DTOs
{
    /*
     * GET /api/episodes/lookup?season=4&episodeNumber=11
     *
     * Just the episode info, no quotes.
     * Used by the quote entry frontend to show the episode title.
     */
    public class EpisodeSummaryDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Season { get; set; }
        public int EpisodeNumber { get; set; }
    }
}
