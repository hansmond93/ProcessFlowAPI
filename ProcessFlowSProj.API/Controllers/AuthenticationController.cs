using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
    [AllowAnonymous]
    [Route("api/auth/")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _iAuthRepo;
        private readonly IConfiguration _iConfig;
        private readonly UserManager<StaffEntity> _userManager;
        private readonly SignInManager<StaffEntity> _signInManager;
        private readonly ITokenDecryptionHelper _token;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthenticationRepository iAuthRepo,
                                        IConfiguration iConfig,
                                        UserManager<StaffEntity> userManager,
                                        SignInManager<StaffEntity> signInManager,
                                        ITokenDecryptionHelper token,
                                        IMapper mapper)
        {
            _iAuthRepo = iAuthRepo;
            _iConfig = iConfig;
            _userManager = userManager;
            _signInManager = signInManager;
            _token = token;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(StaffForRegisterationDto userForReg)
        {
            try
            {
                var userToCreate = new StaffEntity
                {
                    FirstName = userForReg.FirstName,
                    LastName = userForReg.LastName,
                    MiddleName = userForReg.MiddleName,
                    UserName = userForReg.Username,
                    Gender = userForReg.Gender,
                    RoleEntityId = userForReg.RoleEntityId
                };

                var result = await _userManager.CreateAsync(userToCreate, userForReg.Password);

                var createdUser = _mapper.Map<CreatedStaffForReturnDto>(userToCreate);

                if (result.Succeeded)
                {
                    return Ok(createdUser);  //modify to 201
                }
                
                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(StaffForLoginDto userForLogin)
        {
            var user = await _userManager.FindByNameAsync(userForLogin.Username);

            var loginResult = await _signInManager.CheckPasswordSignInAsync(user, userForLogin.Password, false);

            if(loginResult.Succeeded)
            {
                var token = await _iAuthRepo.GetToken(user);

                return Ok(new { token });
            }

            return Unauthorized(); 
            
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