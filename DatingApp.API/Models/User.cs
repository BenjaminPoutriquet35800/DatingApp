using System;

namespace DatingApp.API.Models
{
    public class User
    {
        public Int32 Id { get; set; }
        public String Username { get; set; }

        public Byte[] PasswordHash { get; set; }

        public Byte[] PasswordSalt { get; set; }
    }
}