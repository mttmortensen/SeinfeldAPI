namespace SeinfeldAPI.Models.DTOs
{
    /*
     * GET /api/quotes/recent
     *
     * This Dto is what the quote entry frontend reads back.
     * Season and EpisodeNumber are numbers here, even though
     * they are stored as strings on the Episode.
     */
    public class QuoteEntryDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Speaker { get; set; }
        public int EpisodeId { get; set; }
        public int Season { get; set; }
        public int EpisodeNumber { get; set; }
        public string EpisodeTitle { get; set; }
    }
}
