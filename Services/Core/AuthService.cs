using SeinfeldAPI.Interfaces;
using SeinfeldAPI.Services.Security;
using SeinfeldAPI.Models;
using BCrypt.Net;

namespace SeinfeldAPI.Services.Core
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly JwtHelper _jwtHelper;

        public AuthService(IUserRepository userRepo, JwtHelper jwtHelper)
        {
            _userRepo = userRepo;
            _jwtHelper = jwtHelper;
        }

        public bool Register(string username, string password) 
        {
            if (_userRepo.GetUserByUsername(username) != null)
                return false;

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            User newUser = new User 
            {
                Username = username,
                PasswordHash = hashedPassword
            };

            _userRepo.AddUser(newUser);
            return _userRepo.SaveChanges();
        }

        public string Login(string username, string password) 
        {
            User user = _userRepo.GetUserByUsername(username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            return _jwtHelper.GenerateToken(username);
        }
    }
}
