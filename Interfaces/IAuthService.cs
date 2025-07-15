namespace SeinfeldAPI.Interfaces
{
    public interface IAuthService
    {
        bool Register(string username, string password);
        string Login(string username, string password);
    }
}
