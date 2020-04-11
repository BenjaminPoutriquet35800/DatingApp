using System.IO;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using DatingApp.API.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace DatingApp.API.Data
{
    public class Seed
    {
        public static void SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (userManager.Users.Any())
                return;

            var userData = File.ReadAllText("Data/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);

            var roles = new List<Role>
            {
                new Role{Name = "Member"},
                new Role{Name = "Admin"},
                new Role{Name = "Moderator"},
                new Role{Name = "VIP"}
            };

            foreach (var role in roles)
            {
                roleManager.CreateAsync(role).Wait();
            }

            foreach (var user in users)
            {
                user.Photos.SingleOrDefault().IsApproved = true;
                userManager.CreateAsync(user, "password").Wait();
                userManager.AddToRoleAsync(user, "Member").Wait();
            }

            // SUDO SU
            var superAdmin = new User
            {
                UserName = "Admin"
            };
            userManager.CreateAsync(superAdmin, "password").Wait();
            userManager.AddToRolesAsync(superAdmin, new[]
            {
                "Admin",
                "Moderator"
            }).Wait();
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}