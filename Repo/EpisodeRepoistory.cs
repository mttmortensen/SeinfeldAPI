using Microsoft.EntityFrameworkCore;
using SeinfeldAPI.Data;
using SeinfeldAPI.Interfaces;
using SeinfeldAPI.Models;

namespace SeinfeldAPI.Repo
{
    public class EpisodeRepoistory : IEpisodeRepository
    {
        // Holds a reference to the db for querying 
        private readonly SeinfeldDbContext _context;
    }
}
