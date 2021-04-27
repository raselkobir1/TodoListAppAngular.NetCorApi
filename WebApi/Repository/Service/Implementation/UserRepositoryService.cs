using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Repository.Service.Interface;

namespace WebApi.Repository.Service.Implementation
{
    public class UserRepositoryService : IUserRepositoryService
    {
        private readonly DataContext dc;

        public UserRepositoryService(DataContext dc)
        {
            this.dc = dc;
        }
        public async Task<User> Authenticate(string userName, string passwordText) 
        {
            var user = await dc.Users.FirstOrDefaultAsync(u=>u.UserName == userName);
            if(user == null || user.PasswordKey == null)
            {
                return null;
            }

            var matchPassword = MatchPasswordHash(passwordText, user.Password, user.PasswordKey);
            if (!matchPassword)
            {
                return null;
            }
            else
            {
                return user;
            }
        }

        private bool MatchPasswordHash(string passwordText, byte[] password, byte[] passwordKey)
        {
            using (var hmac = new HMACSHA512(passwordKey))
            {
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordText));

                for (int i = 0; i < passwordHash.Length; i++) 
                {
                    if (passwordHash[i] != password[i])
                        return false;
                }
                return true;
            }
        }

        public void Register(string userName, string password,string role)
        {
            byte[] passwordHash, passwordKey;
            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            User user = new User();
            user.UserName = userName;
            user.Password = passwordHash;
            user.PasswordKey = passwordKey;
            user.Role = role;

            dc.Users.Add(user);
        }

        public async Task<bool> UserAlreadyExists(string userName)
        {
            return await dc.Users.AnyAsync(a=>a.UserName == userName);
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return await dc.Users.ToListAsync();
        }

        public async Task<User> FindUserById(int id)
        {
            var user = dc.Users.FindAsync(id);
            return await user;
        }
    }
}
