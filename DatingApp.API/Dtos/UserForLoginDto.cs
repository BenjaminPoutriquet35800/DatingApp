using System;
using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForLoginDto
    {
        [Required]
        public String Username { get; set; }

        [Required]
        public String Password { get; set; }
    }
}