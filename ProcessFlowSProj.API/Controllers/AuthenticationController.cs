using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProcessFlowSProj.API.Common;
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
        private readonly UserManager<StaffEntity> _userManager;
        private readonly SignInManager<StaffEntity> _signInManager;
        private readonly ITokenDecryptionHelper _token;

        public AuthenticationController(IAuthenticationRepository iAuthRepo,
                                        IConfiguration iConfig,
                                        UserManager<StaffEntity> userManager,
                                        SignInManager<StaffEntity> signInManager,
                                        ITokenDecryptionHelper token)
        {
            _iAuthRepo = iAuthRepo;
            _iConfig = iConfig;
            _userManager = userManager;
            _signInManager = signInManager;
            _token = token;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(StaffForRegisterationDto userForReg)      //not on point check REPO
        {
            try
            {
                //Validate Request
                userForReg.Username = userForReg.Username.Trim().ToLower();

                if (await _iAuthRepo.UserExists(userForReg.Username))
                    return BadRequest("Username already exists");

                var userToCreate = new StaffEntity
                {
                    FirstName = userForReg.FirstName,
                    LastName = userForReg.LastName,
                    MiddleName = userForReg.MiddleName,
                    UserName = userForReg.Username,
                    Gender = userForReg.Gender,
                    RoleId = userForReg.RoleId
                };

                var createdUser = await _iAuthRepo.Register(userToCreate, userForReg.Password);

                if (createdUser == null)
                    return StatusCode(500);

                return Ok(createdUser); //modify this to return 201
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(StaffForLoginDto userForLogin)
        {
            var userFromRepo = await _iAuthRepo.Login(userForLogin.Username, userForLogin.Password);

            if(userFromRepo == null)
                return Unauthorized();

            var loginResult = await _signInManager.PasswordSignInAsync(userForLogin.Username, userForLogin.Password, isPersistent: false, lockoutOnFailure: false);

            if(!loginResult.Succeeded)
                return Unauthorized();

            var userFromManager = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userFromRepo.StaffId);

            if (userFromManager == null)
            {
                return Unauthorized();  //check for other kindda trouble
            }
            var token = _iAuthRepo.GetToken(userFromManager);

            return Ok(token);
        }

        [HttpPost("logout")]    //test this method
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            return Ok("Done !!!");
        }

        [Authorize]
        [HttpPost]
        [Route("refreshtoken")]
        public async Task<IActionResult> RefreshToken()
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == _token.GetStaffId());

            var token = _iAuthRepo.GetToken(user);

            return Ok(token);

        }

    }
}