using Microsoft.AspNetCore.Identity;
using ProcessFlowSProj.API.Dtos;
using ProcessFlowSProj.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Interface
{
    public interface IAuthenticationRepository
    {
        Task<CreatedStaffForReturnDto> Register(StaffEntity user, string password);
        Task<StaffLoginEntity> Login(string username, string password);
        Task<bool> UserExists(string username);
        Task<StaffEntity> GetUserDetailsWithUserLoginId(int id);
        string GetToken(StaffEntity user);
    }
}