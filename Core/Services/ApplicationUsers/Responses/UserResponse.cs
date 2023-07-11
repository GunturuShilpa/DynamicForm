namespace Core.Services.ApplicationUsers.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public int RoleId { get; set; } = 0;
        public DateTime? CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
    }
}
