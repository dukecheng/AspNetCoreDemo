using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace AspNetCoreDemo.ShareWebFramework.Sessions
{
    public class UserSession : IUserSession
    {
        public HttpContext HttpContext { get; }

        public UserSession(IHttpContextAccessor httpContextAccessor)
        {
            HttpContext = httpContextAccessor.HttpContext;
        }

        public ClaimsIdentity ClaimsIdentity
        {
            get
            {
                if (HttpContext.User.Identity is ClaimsIdentity)
                {
                    return HttpContext.User.Identity as ClaimsIdentity;
                }
                else
                {
                    throw new UnauthorizedAccessException();
                }
            }
        }

        public bool IsAuthenticated => HttpContext.User.Identity.IsAuthenticated;

        public string DisplayName => GetClaimsItemValueAs<string>(ClaimNames.FullName);

        public string UserId => GetClaimsItemValueAs<string>(ClaimTypes.Name);

        public string Email => GetClaimsItemValueAs<string>(ClaimTypes.Name);

        private T GetClaimsItemValueAs<T>(string itemName)
        {
            var claimItemValue = ClaimsIdentity.Claims?.FirstOrDefault(x => x.Type == itemName)?.Value;
            if (!string.IsNullOrWhiteSpace(claimItemValue))
            {
                return (T)Convert.ChangeType(claimItemValue, typeof(T));
            }
            return default(T);
        }
    }
}
