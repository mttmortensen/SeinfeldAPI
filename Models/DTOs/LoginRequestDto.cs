using SeinfeldAPI.Services.Attributes;

namespace SeinfeldAPI.Models.DTOs
{
    public class LoginRequestDto
    {
        [ValidUsername]
        public string Username { get; set; }

        [ValidPassword]
        public string Password { get; set; }
    }
}
