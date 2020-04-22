using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProcessFlowSProj.API.Data;
using ProcessFlowSProj.API.Dtos;
using ProcessFlowSProj.API.Entities;
using ProcessFlowSProj.API.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<StaffEntity> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthenticationRepository(DataContext context, UserManager<StaffEntity> userManager, IMapper mapper, IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _config = config;
        }
        public async Task<StaffLoginEntity> Login(string username, string password)
        {
            var user = await _context.StaffLoginEntities.FirstOrDefaultAsync(x => x.StaffEntity.UserName.ToLower() == username.ToLower());

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;
            return user;
        }


        public async Task<CreatedStaffForReturnDto> Register(StaffEntity user, string password)
        {
            //I might have to use a roll back method incase all after creating a user, a logIn entity is unable to be created
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt); //this syntax automatically defines the passworldSalt and passwordHash so they ara available in this method

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)   //I have to roll back or check if the username exists and there's no login entity using Update method of userManager
            {
                var userLogin = new StaffLoginEntity
                {
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    StaffId = user.Id           //check if this Id is populated
                };

                await _context.StaffLoginEntities.AddAsync(userLogin);

                if (await _context.SaveChangesAsync() > 0)
                {
                    user.StaffLoginEntityId = userLogin.StaffLoginId;
                }

                if (await _context.SaveChangesAsync() > 0)
                {
                    var staffToREturn = _mapper.Map<CreatedStaffForReturnDto>(user);

                    return staffToREturn;
                }
                    
            }

            return null;
        }

        public string GetToken(StaffEntity user)
        {
            var claims = new[]
           {

                new Claim("Username", user.UserName),
                new Claim("StaffId", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(claims),
            //    Expires = DateTime.Now.AddDays(1),  //add this to aooSettings.json file
            //    SigningCredentials = creds
            //};

            var tokenDescriptor = new JwtSecurityToken(
                signingCredentials: creds,
                claims: claims,
                expires: DateTime.Now.AddDays(1));

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(tokenDescriptor);
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.StaffEntities.AnyAsync(x => x.UserName.ToLower() == username.ToLower()))
                return true;

            return false;
        }

        public async Task<StaffEntity> GetUserDetailsWithUserLoginId(int id)
        {
            var userDetails = await _context.StaffEntities.FirstOrDefaultAsync(x => x.StaffLoginEntityId == id);

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
