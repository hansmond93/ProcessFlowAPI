using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProcessFlowSProj.API.Dtos;
using ProcessFlowSProj.API.Entities;
using ProcessFlowSProj.API.Interface;

namespace ProcessFlowSProj.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _iAuthRepo;
        private readonly IConfiguration _iConfig;
        public AuthenticationController(IAuthenticationRepository iAuthRepo, IConfiguration iConfig)
        {
            _iAuthRepo = iAuthRepo;
            _iConfig = iConfig;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(StaffForRegisterationDto userForReg)
        {
            //Validate Request
            userForReg.Username = userForReg.Username.ToLower();

            if (await _iAuthRepo.UserExists(userForReg.Username))
                return BadRequest("Username already exists");

            var userToCreate = new StaffEntity
            {
                FirstName = userForReg.FirstName,
                LastName = userForReg.LastName,
                Username = userForReg.Username
            };

            var createdUser = await _iAuthRepo.Register(userToCreate, userForReg.Password);

            if (createdUser == null)
                return StatusCode(500);

            return StatusCode(201);
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(StaffForLoginDto userForLogin)
        {
            var userFromRepo = await _iAuthRepo.Login(userForLogin.Username, userForLogin.Password);

            if(userFromRepo == null)
            {
                return Unauthorized();
            }
            var userFromRepoDetails = await _iAuthRepo.GetUserDetailsWithUserLoginId(userFromRepo.StaffLoginId);
            if (userFromRepoDetails == null)
            {
                return Unauthorized();  //check for other kindda trouble
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepoDetails.StaffId.ToString()),
                new Claim(ClaimTypes.Name, userFromRepoDetails.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iConfig.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
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