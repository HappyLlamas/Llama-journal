﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using llama_journal.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace llama_journal.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ModelsContext _context;

        public UserRepository(ModelsContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User GetById(string userId)
        {
            return _context.Users.Find(userId);
        }

        public User FindByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public void SetGroup(User user, string groupName)
        {
            user.Group = _context.Groups.FirstOrDefault(g => g.Name == groupName);
            _context.SaveChanges();
        }

        public void SetRole(User user, RoleEnum role)
        {
            user.Role = role;
            _context.SaveChanges();
        }

        public void CreateUser(string email, string fullname)
        {
            var user = new User { Email = email, FullName = fullname };
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void SetUserPassword(User user, string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
    
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            user.PasswordSalt = Convert.ToBase64String(salt);
            user.Password = hashedPassword;

            _context.SaveChanges();
        }

        public bool CheckPassword(User user, string password)
        {
            byte[] salt = Convert.FromBase64String(user.PasswordSalt);
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
    
            return user.Password == hashedPassword;
        }
    }
}