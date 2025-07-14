using SeinfeldAPI.Data;
using SeinfeldAPI.Interfaces;

namespace SeinfeldAPI.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly SeinfeldDbContext _context;

        public UserRepository(SeinfeldDbContext context)
        {
            _context = context;
        }
    }
}
