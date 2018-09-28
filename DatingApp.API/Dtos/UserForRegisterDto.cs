using System;
using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public String Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Mot de passe doit-être compris entre 4 - 8 caractères")]
        public String Password { get; set; }
    }
}