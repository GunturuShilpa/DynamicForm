namespace Core.Services.Authentication.Responses
{
    public class AuthResponse
    {
        public SessionObject sessionObject { get; set; }
    }

    public class SessionObject
    {
        public int userId { get; set; }
        public int roleId { get; set; }
        public string userName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;

    }
}
