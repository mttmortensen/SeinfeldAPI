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
        public string Title { get; set; }
        public string Season { get; set; }
        public string EpisodeNumber { get; set; }
        public DateTime AirDate { get; set; }

        // Including now the simplifed EpisodeQuotes data
        public List<QuoteInlineDto> Quotes { get; set; }
    }
}
