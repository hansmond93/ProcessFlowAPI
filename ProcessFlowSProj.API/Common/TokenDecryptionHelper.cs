using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;

namespace ProcessFlowSProj.API.Common
{
    public class TokenDecryptionHelper
    {
        //public TokenDecryptionHelper(IHttpContextAccessor httpContextAccessor)
        //{
        //    _contextAccessor = httpContextAccessor;
        //}
        private readonly IHttpContextAccessor _contextAccessor;
        private object DecrpytToken(int tokenNumber)
        {
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
}
