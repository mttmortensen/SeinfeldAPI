namespace SeinfeldAPI.Models.DTOs
{
    /*
     * POST /api/episodequotes
     * 
     * PUT /api/episodequotes
     * 
     * This Dto is to be used when Adding (POST) or
     * Updating (PUT) a new EpisodeQuote. By using 
     * this Dto, we can still allow the relation 
     * between Episode and EpisodeQuote to happen.
     */
    public class EpisodeQuoteDto
    {
        public int Id { get; set; }
        public string Quote { get; set; }
        public string Character { get; set; }
        public int? EpisodeId { get; set; }
        public string? EpisodeTitle { get; set; }
        public string? EpisodeSeason { get; set; }
    }
}
