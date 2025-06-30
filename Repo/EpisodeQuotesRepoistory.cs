using SeinfeldAPI.Data;
using SeinfeldAPI.Interfaces;

namespace SeinfeldAPI.Repo
{
    public class EpisodeQuotesRepoistory: IEpisodeQuotesRepository
    {
        // Reference to the db via the context
        private readonly SeinfeldDbContext _context;

        // Construct injecting the context
        public EpisodeQuotesRepoistory(SeinfeldDbContext context) 
        {
            _context = context;
        }
    }
}
