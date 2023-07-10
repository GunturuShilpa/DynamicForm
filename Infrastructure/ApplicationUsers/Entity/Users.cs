﻿using Infrastructure.Base.Entity;

namespace Infrastructure.ApplicationUsers.Entity
{
    public class Users : TableEntity
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public int RoleId { get; set; } = 0;
    }
}
