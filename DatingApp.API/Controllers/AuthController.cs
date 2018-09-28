using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;

            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            Boolean userExist = await _authRepository.UserExists(userForRegisterDto.Username);

            if (userExist)
                return BadRequest("L'utilisateur existe déjà avec ce login");

            var userToCreate = new User()
            {
                Username = userForRegisterDto.Username,
            };

            var createdUser = await _authRepository.Register(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userResponse = await _authRepository.Login(userForLoginDto.Username, userForLoginDto.Password);

            if (userResponse == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userResponse.Id.ToString()),
                new Claim(ClaimTypes.Name,userForLoginDto.Username)
            };

            String secretKey = _configuration.GetSection("AppSettings:Token").Value;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new 
            {
                token = tokenHandler.WriteToken(token)
            });
        }

    }
}