using SeinfeldAPI.Data;
using SeinfeldAPI.Interfaces;
using SeinfeldAPI.Models;


namespace SeinfeldAPI.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly SeinfeldDbContext _context;

        public UserRepository(SeinfeldDbContext context)
        {
            _context = context;
        }

        public List<User> GetAllUsers() 
        {
            return _context.Users
                .ToList();
        }

        public User GetUserById(int id) 
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username.ToLower());
        }

        public bool AddUser(User user) 
        {
            _context.Users.Add(user);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateUser(User user) 
        {
            _context.Users.Update(user);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteUser(int id) 
        {
            User user = _context.Users.Find(id);

            if (user == null) 
            {
                _context.Users.Remove(user);
                return _context.SaveChanges() > 0;
            }

            return false;
        }

        public bool SaveChanges() 
        {
            _context.SaveChanges();
            return true;
        }
    }
}
