using System.ComponentModel.DataAnnotations;

namespace Core.Services.ApplicationUsers.Requests
{
    public class CreateUserRequest
    {
        public int Id { get; set; } = 0;
        [Required]
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; } = 0;
    }
}
