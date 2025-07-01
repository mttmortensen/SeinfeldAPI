using SeinfeldAPI.Repo;

namespace SeinfeldAPI.Services
{
    public class EpisodeQuotesService
    {
        private readonly EpisodeQuotesRepoistory _eqrepo;

        public EpisodeQuotesService(EpisodeQuotesRepoistory eqrepo) 
        {
            _eqrepo = eqrepo;
        }
    }
}
