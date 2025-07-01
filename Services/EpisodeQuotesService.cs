using SeinfeldAPI.Repo;

namespace SeinfeldAPI.Services
{
    public class EpisodeQuotesService
    {
        private readonly EpisodeQuotesRepoistory _quotesRepo;
        private readonly EpisodeRepoistory _episodeRepo;

        public EpisodeQuotesService(EpisodeQuotesRepoistory quotesRepo, EpisodeRepoistory episodeRepo) 
        {
            _quotesRepo = quotesRepo;
            _episodeRepo = episodeRepo;
        }
    }
}
