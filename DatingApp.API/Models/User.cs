using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DatingApp.API.Models
{
    public class User : IdentityUser<int>
    {
        public Byte[] PasswordSalt { get; set; }
        public String Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public String Introduction { get; set; }
        public String LookingFor { get; set; }
        public String Interests { get; set; }
        public String City { get; set; }
        public String Country { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Like> Likers { get; set; }
        public virtual ICollection<Like> Likees { get; set; }
        public virtual ICollection<Message> MessagesSent { get; set; }
        public virtual ICollection<Message> MessagesReceived { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}