using Microsoft.EntityFrameworkCore;
using ProcessFlowSProj.API.Data;
using ProcessFlowSProj.API.Entities;
using ProcessFlowSProj.API.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly DataContext _context;
        public AuthenticationRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<UserLoginEntity> Login(string username, string password)
        {
            var user = await _context.UserLoginEntities.FirstOrDefaultAsync(x => x.Username == username.ToLower());

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;
            return user;
        }


        public async Task<UserLoginEntity> Register(UserEntity user, string password)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt); //this syntax automatically defines the passworldSalt and passwordHash so they ara available in this method

            var userLogin = new UserLoginEntity
            {
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Username = user.Username.ToLower()
            };

            await _context.UserLoginEntities.AddAsync(userLogin);

            if(await _context.SaveChangesAsync() > 0)
            {
                user.UserLoginEntityId = userLogin.UserLoginId;
            }

            await _context.UserEntities.AddAsync(user);
            if (await _context.SaveChangesAsync() > 0)
            {
                return userLogin;
            }

            return null;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.UserLoginEntities.AnyAsync(x => x.Username.ToLower() == username.ToLower()))
                return true;

            return false;
        }

        public async Task<UserEntity> GetUserDetailsWithUserLoginId(int id)
        {
            var userDetails = await _context.UserEntities.FirstOrDefaultAsync(x => x.UserLoginEntityId == id);

            return userDetails;
        }









        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())    //This is done so as to force the compiler to call the dispose method inherited by the HMACSHA512() class
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); //this passes our password as a byte array to the ComputeHash method 
            }
        }


        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))    //This is done so as to force the compiler to call the dispose method inherited by the HMACSHA512() class
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); //this passes our password as a byte array to the ComputeHash method 
                for(int i=0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }
    }
}
