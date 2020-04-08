using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using System;

namespace ProcessFlowSProj.API.Common
{
    public class TokenDecryptionHelper : ITokenDecryptionHelper
    {
        //public TokenDecryptionHelper(IHttpContextAccessor httpContextAccessor)
        //{
        //    _contextAccessor = httpContextAccessor;
        //}
        private readonly IHttpContextAccessor _contextAccessor;

        public TokenDecryptionHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public int GetStaffId()
        {
            var result =  DecrpytToken(1);

            var output = Convert.ToInt32(result);

            return output;
        }

        private object DecrpytToken(int tokenNumber)
        {
            if (_contextAccessor.HttpContext == null || _contextAccessor.HttpContext.User == null)
                throw new CustomException("User can't be determinded");

            var tokenIdentity = new ClaimsIdentity(_contextAccessor.HttpContext.User.Identity);

            if (tokenIdentity != null)
            {
                var claims = tokenIdentity.Claims;

                switch (tokenNumber)
                {
                    case 0: return claims.FirstOrDefault(x => x.Type == "Username").Value.ToString();
                    case 1: return claims.FirstOrDefault(x => x.Type == "StaffId").Value.ToString();
                    default: return claims.FirstOrDefault(x => x.Type == "StaffId").Value.ToString();
                }
            }
            else
            {
                return string.Empty;
            }

        }

    }

    public interface ITokenDecryptionHelper
    {
        int GetStaffId();
    }
}
