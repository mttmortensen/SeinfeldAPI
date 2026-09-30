namespace SeinfeldAPI.Models
{
    // Outcome of adding/updating a quote from the quote entry frontend
    // Lets the controller pick the right status code (201/204, 400, 404, 409)
    public enum QuoteEntryStatus
    {
        Success,
        NotFound,
        Duplicate,
        EpisodeTitleRequired
    }
}
