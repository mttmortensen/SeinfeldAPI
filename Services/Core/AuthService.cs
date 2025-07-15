using SeinfeldAPI.Interfaces;
using SeinfeldAPI.Services.Security;

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

        }

        public string Login(string username, string password) { }
    }
}
