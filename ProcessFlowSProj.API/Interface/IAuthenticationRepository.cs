using ProcessFlowSProj.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Interface
{
    public interface IAuthenticationRepository
    {
        Task<UserLoginEntity> Register(UserEntity user, string password);
        Task<UserLoginEntity> Login(string username, string password);
        Task<bool> UserExists(string username);
        Task<UserEntity> GetUserDetailsWithUserLoginId(int id);
    }
}