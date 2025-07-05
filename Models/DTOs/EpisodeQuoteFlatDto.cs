namespace SeinfeldAPI.Models.DTOs
{
    /*
     * This Dto is used in GET Requests under Episodes
     * When a user goes to /api/episodes, the quotes block 
     * will pull this Dto as I did not want to have repeated
     * Episode info such as Title and Season if it was already
     * being listed in the parent.
     */
    public class EpisodeQuoteFlatDto
    {
        public int Id { get; set; }
        public string Quote { get; set; }
        public string Character { get; set; }
        public int EpisodeId { get; set; }
    }
}
