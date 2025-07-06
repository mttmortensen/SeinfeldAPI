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
        public string? Season { get; set; }
        public string? EpisodeNumber { get; set; }
        public DateTime? AirDate { get; set; }
    }
}
