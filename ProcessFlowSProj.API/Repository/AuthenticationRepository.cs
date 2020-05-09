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
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<StaffEntity> _signInManager;

        public AuthenticationRepository(DataContext context, UserManager<StaffEntity> userManager, RoleManager<Role> roleManager, 
            SignInManager<StaffEntity> signInManager, IMapper mapper, IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _config = config;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
       
        //public async Task<CreatedStaffForReturnDto> Register(StaffEntity user, string password)
        //{
        //    //I might have to use a roll back method incase all after creating a user, a logIn entity is unable to be created
        //    CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt); //this syntax automatically defines the passworldSalt and passwordHash so they ara available in this method

        //    var result = await _userManager.CreateAsync(user, password);

        //    if (result.Succeeded)   //I have to roll back or check if the username exists and there's no login entity using Update method of userManager
        //    {
        //        var userLogin = new StaffLoginEntity
        //        {
        //            PasswordHash = passwordHash,
        //            PasswordSalt = passwordSalt,
        //            StaffId = user.Id           //check if this Id is populated
        //        };

        //        await _context.StaffLoginEntities.AddAsync(userLogin);

        //        if (await _context.SaveChangesAsync() > 0)
        //        {
        //            user.StaffLoginEntityId = userLogin.StaffLoginId;
        //        }

        //        if (await _context.SaveChangesAsync() > 0)
        //        {
        //            var staffToREturn = _mapper.Map<CreatedStaffForReturnDto>(user);

        //            return staffToREturn;
        //        }
                    
        //    }

        //    return null;
        //}

        public async Task<string> GetToken(StaffEntity user)
        {
            var claims = new List<Claim>
            {

                new Claim("Username", user.UserName),
                new Claim("StaffId", user.Id.ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),  //add this to aooSettings.json file
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        //public async Task<bool> UserExists(string username)
        //{
        //    if (await _context.StaffEntities.AnyAsync(x => x.UserName.ToLower() == username.ToLower()))
        //        return true;

        //    return false;
        //}

        //public async Task<StaffEntity> GetUserDetailsWithUserLoginId(int id)
        //{
        //    var userDetails = await _context.StaffEntities.FirstOrDefaultAsync(x => x.StaffLoginEntityId == id);

        //    return userDetails;
        //}









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
